using System.Net;
using System.Text;
using System.Text.Json;

using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Lambda.Core;

using Cognito.Events.Shared.Core;
using Cognito.Events.Shared.Events;
using Cognito.Events.Shared.Exceptions;

namespace Cognito.PostAuthentication;

// TODO: Add retries
public class PostAuthenticationHandler : CognitoTriggerHandler<PostAuthenticationEvent>
{
    public const string TriggerSourceName = "PostAuthentication_Authentication";
    public const string Uri = "https://api.strivitech.me/";
    public const string RelateUserRegistrationFlowUrl = "Auth/RelateUserRegistrationFlow";
    private const string AppLinkingName = "MuHub";
    private const string UserAttributesUserName = "userName";
    private const string ApplicationUserIdName = "applicationUserId";
    private const string ApplicationUserIdAttributeName = "custom:ApplicationUserId";
    private const uint PooledConnectionLifetimeMinutes = 1;
    
    private readonly IAmazonCognitoIdentityProvider _cognitoProvider;

    // HttpClient only resolves DNS entries when a connection is created
    private static readonly HttpClient HttpClient = new(new SocketsHttpHandler
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(PooledConnectionLifetimeMinutes)
    })
    {
        BaseAddress = new Uri(Uri)
    };
    
    public override string TriggerSource => TriggerSourceName;

    public PostAuthenticationHandler(
        JsonElement cognitoEvent,
        ILambdaLogger logger,
        IAmazonCognitoIdentityProvider cognitoProvider)
        : base(cognitoEvent, logger)
    {
        _cognitoProvider = cognitoProvider ?? throw new ArgumentNullException(nameof(cognitoProvider));
    }

    public override async Task<JsonElement> HandleTriggerEventAsync()
    {
        var identityUserName = TriggerEvent.Request?.UserAttributes?[UserAttributesUserName];
        ValidateIdentityUserName(identityUserName);

        if (AppUserIdLinked())
        {
            Logger.LogInformation($"UserName {identityUserName} has been already linked to app {AppLinkingName}");
            return await base.HandleTriggerEventAsync();
        }
        
        Logger.LogDebug("Started handling a post authentication trigger event for user with UserName: " +
                        $"{identityUserName}. Trying to link a user to app {AppLinkingName}");

        try
        {
            var userLinkRegistrationData = await PostRelateUserRegistrationFlow(identityUserName);

            if (string.IsNullOrEmpty(userLinkRegistrationData?.ApplicationUserId))
            {
                Logger.LogError("Linking was not successful. ApplicationUserId is null or empty");

                throw new CognitoPostAuthenticationException(
                    $"Linking to UserName {identityUserName} was not successful." +
                    "Application user id is null or empty");
            }

            await AttachApplicationUserIdToIdentityUser(identityUserName, userLinkRegistrationData.ApplicationUserId);
            
            Logger.LogInformation("Successfully linked the user with UserName: " +
                            $"{identityUserName} to app {AppLinkingName}");
            
            return await base.HandleTriggerEventAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError("Failed to relate a user registration flow." +
                            $"Exception: {JsonSerializer.Serialize(ex)}");
            throw new CognitoPostAuthenticationException("Failed to relate a user registration flow", ex);
        }
    }

    private async Task<UserLinkRegistrationDataResponse?> PostRelateUserRegistrationFlow(string? identityUserName)
    {
        var response = await HttpClient.PostAsync(
            requestUri: RelateUserRegistrationFlowUrl,
            content: new StringContent(
                content: JsonSerializer.Serialize(identityUserName),
                encoding: Encoding.UTF8,
                mediaType: "application/json"
            ));
        
        Logger.LogDebug("Response http code from linking the user with UserName: " +
                        $"{identityUserName} to app {AppLinkingName} is: {response.StatusCode}");
        
        if (!response.IsSuccessStatusCode)
        {
            Logger.LogError("Failed to relate a user registration flow." +
                            $"Response {JsonSerializer.Serialize(response)}");
            throw new CognitoPostAuthenticationException("Failed to relate user registration flow");
        }
        
        await using var contentStream = await response.Content.ReadAsStreamAsync();
        var userLinkRegistrationData = JsonSerializer.Deserialize<UserLinkRegistrationDataResponse>(contentStream);
            
        Logger.LogDebug("Successfully got a response from a relating API." +
                        $"Response {JsonSerializer.Serialize(userLinkRegistrationData)}");

        return userLinkRegistrationData;
    }

    private async Task AttachApplicationUserIdToIdentityUser(string? identityUserName, string? applicationUserId)
    {
        var attribute = new AttributeType
        {
            Name = ApplicationUserIdAttributeName,
            Value = applicationUserId
        };

        var request = new AdminUpdateUserAttributesRequest
        {
            UserPoolId = TriggerEvent.UserPoolId,
            Username = identityUserName,
            UserAttributes = new List<AttributeType> { attribute }
        };

        var response = await _cognitoProvider.AdminUpdateUserAttributesAsync(request);

        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            Logger.LogError("Failed to bind an application user id to UserAttributes: " +
                            $"Identity UserName: {identityUserName}. Response {JsonSerializer.Serialize(response)}");
            throw new CognitoPostAuthenticationException("Failed to attach an application user id to a user");
        }
    }

    private bool AppUserIdLinked()
    {
        string? applicationUserIdValue = null;
        var containsApplicationUserId = TriggerEvent.Request?.UserAttributes?
            .TryGetValue(ApplicationUserIdName, out applicationUserIdValue);

        if (containsApplicationUserId is null || !containsApplicationUserId.Value)
        {
            throw new InvalidOperationException(
                $"Field {nameof(containsApplicationUserId)} is not accessible in the user attributes." +
                "Check if UserAttributes exists in the request.");
        }

        return !string.IsNullOrEmpty(applicationUserIdValue);
    }
    
    private void ValidateIdentityUserName(string? identityUserName)
    {
        if (string.IsNullOrEmpty(identityUserName))
        {
            Logger.LogError("Identity UserName is null or empty." +
                            $"TriggerEvent {JsonSerializer.Serialize(TriggerEvent)}");
            throw new CognitoPostAuthenticationException("Identity UserName is null or empty");
        }
    }
}
