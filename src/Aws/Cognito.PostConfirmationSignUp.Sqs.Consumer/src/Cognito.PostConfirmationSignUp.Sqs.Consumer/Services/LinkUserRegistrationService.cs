using System.Net;
using System.Text;
using System.Text.Json;

using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Lambda.Core;
using Amazon.Runtime.Internal.Util;

using Cognito.Common;
using Cognito.Events.Shared.Exceptions;
using Cognito.PostConfirmationSignUp.Sqs.Consumer.Responses;

namespace Cognito.PostConfirmationSignUp.Sqs.Consumer.Services;

public class LinkUserRegistrationService : ILinkUserRegistrationService
{
    private readonly IAmazonCognitoIdentityProvider _cognitoProvider;
    private readonly ILambdaLogger _logger;

    // HttpClient only resolves DNS entries when a connection is created
    private static readonly HttpClient HttpClient = new(new SocketsHttpHandler
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(Config.PooledConnectionLifetimeMinutes)
    }) { BaseAddress = new Uri(Config.Uri) };

    public LinkUserRegistrationService(IAmazonCognitoIdentityProvider cognitoProvider, ILambdaLogger logger)
    {
        _cognitoProvider = cognitoProvider ?? throw new ArgumentNullException(nameof(cognitoProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<string> LinkAsync(string identityUserName, string cognitoPoolId)
    {
        ValidateIdentityUserName(identityUserName);
        ValidateCognitoPoolId(cognitoPoolId);

        _logger.LogDebug("Started handling a post authentication trigger event for user with UserName: " +
                         $"{identityUserName}. Trying to link a user to app {Config.AppLinkingName}");

        try
        {
            var userLinkRegistrationData = await PostRelateUserRegistrationFlow(identityUserName);

            if (string.IsNullOrEmpty(userLinkRegistrationData?.ApplicationUserId))
            {
                _logger.LogError("Linking was not successful. ApplicationUserId is null or empty");

                throw new CognitoPostConfirmationSignUpException(
                    $"Linking to UserName {identityUserName} was not successful." +
                    "Application user id is null or empty");
            }

            await AttachApplicationUserIdToIdentityUser(identityUserName, userLinkRegistrationData.ApplicationUserId,
                cognitoPoolId);

            _logger.LogInformation("Successfully linked the user with UserName: " +
                                   $"{identityUserName} to app {Config.AppLinkingName}");

            return userLinkRegistrationData.ApplicationUserId;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to relate a user registration flow." +
                             $"Exception: {JsonSerializer.Serialize(ex)}");
            throw new CognitoPostConfirmationSignUpException("Failed to relate a user registration flow", ex);
        }
    }

    private async Task<UserLinkRegistrationDataResponse?> PostRelateUserRegistrationFlow(string? identityUserName)
    {
        var response = await HttpClient.PostAsync(
            requestUri: Config.RelateUserRegistrationFlowUrl,
            content: new StringContent(
                content: JsonSerializer.Serialize(identityUserName),
                encoding: Encoding.UTF8,
                mediaType: "application/json"
            ));

        _logger.LogDebug("Response http code from linking the user with UserName: " +
                         $"{identityUserName} to app {Config.AppLinkingName} is: {response.StatusCode}");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to relate a user registration flow." +
                             $"Response {JsonSerializer.Serialize(response)}");
            throw new CognitoPostConfirmationSignUpException("Failed to relate user registration flow");
        }

        await using var contentStream = await response.Content.ReadAsStreamAsync();
        var userLinkRegistrationData = JsonSerializer.Deserialize<UserLinkRegistrationDataResponse>(contentStream);

        _logger.LogDebug("Successfully got a response from a relating API." +
                         $"Response {JsonSerializer.Serialize(userLinkRegistrationData)}");

        return userLinkRegistrationData;
    }

    private async Task AttachApplicationUserIdToIdentityUser(
        string? identityUserName,
        string? applicationUserId,
        string? cognitoPoolId)
    {
        var attribute = new AttributeType { Name = UserAttributesNames.ApplicationUserId, Value = applicationUserId };

        var request = new AdminUpdateUserAttributesRequest
        {
            UserPoolId = cognitoPoolId,
            Username = identityUserName,
            UserAttributes = new List<AttributeType> { attribute }
        };

        var response = await _cognitoProvider.AdminUpdateUserAttributesAsync(request);

        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            _logger.LogError("Failed to bind an application user id to UserAttributes: " +
                             $"Identity UserName: {identityUserName}. Response {JsonSerializer.Serialize(response)}");
            throw new CognitoPostConfirmationSignUpException("Failed to attach an application user id to a user");
        }
    }

    private void ValidateIdentityUserName(string? identityUserName)
    {
        if (string.IsNullOrEmpty(identityUserName))
        {
            _logger.LogError("Identity UserName is null or empty");
            throw new ArgumentException("Identity UserName is null or empty");
        }
    }
    
    private void ValidateCognitoPoolId(string? cognitoPoolId)
    {
        if (string.IsNullOrEmpty(cognitoPoolId))
        {
            _logger.LogError("CognitoPoolId is null or empty");
            throw new ArgumentException("Identity UserName is null or empty");
        }
    }
}
