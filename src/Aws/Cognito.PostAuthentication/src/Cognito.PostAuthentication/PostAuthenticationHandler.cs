using System.Text;
using System.Text.Json;

using Amazon.Lambda.Core;

using Cognito.Events.Shared.Core;
using Cognito.Events.Shared.Events;
using Cognito.Events.Shared.Exceptions;

namespace Cognito.PostAuthentication;

public class PostAuthenticationHandler : CognitoTriggerHandler<PostAuthenticationEvent>
{
    public const string TriggerSourceName = "PostAuthentication_Authentication";
    public const string Uri = "https://api.strivitech.me";
    private const string AppLinkingName = "MuHub";
    private const string UserAttributesSubName = "sub";
    private const string ApplicationUserIdName = "applicationUserId";
    private const uint PooledConnectionLifetimeValue = 1;
    
    // HttpClient only resolves DNS entries when a connection is created
    private static readonly HttpClient HttpClient = new(new SocketsHttpHandler
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(PooledConnectionLifetimeValue)
    })
    {
        BaseAddress = new Uri(Uri)
    };
    
    public override string TriggerSource => TriggerSourceName;

    public PostAuthenticationHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }

    public override async Task<JsonElement> HandleTriggerEventAsync()
    {
        var identityUserId = TriggerEvent.Request?.UserAttributes?[UserAttributesSubName];

        if (string.IsNullOrEmpty(identityUserId))
        {
            Logger.LogError("Identity user id is null or empty." +
                            $"TriggerEvent {JsonSerializer.Serialize(TriggerEvent)}");
            throw new CognitoPostAuthenticationException("Identity user id is null or empty");
        }

        if (AppUserIdLinked())
        {
            Logger.LogInformation($"User Identity Id {identityUserId} has been already linked to app {AppLinkingName}");
            return await base.HandleTriggerEventAsync();
        }
        
        Logger.LogDebug("Started handling post authentication trigger event for user with identity user id: " +
                        $"{identityUserId}. Trying to link user to app with name: {AppLinkingName}");

        try
        {
            var userLinkRegistrationData = await PostRelateUserRegistrationFlow(identityUserId);

            if (string.IsNullOrEmpty(userLinkRegistrationData?.ApplicationUserId))
            {
                Logger.LogError("Linking was not successful. Application user id is null or empty");

                throw new CognitoPostAuthenticationException(
                    $"Linking to identityUserId {identityUserId} was not successful." +
                    "Application user id is null or empty");
            }
            
            Logger.LogInformation("Successfully linked user with identity user id: " +
                            $"{identityUserId} to app with name: {AppLinkingName}");
            
            return await base.HandleTriggerEventAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError("Failed to relate user registration flow." +
                            $"Exception: {JsonSerializer.Serialize(ex)}");
            throw new CognitoPostAuthenticationException("Failed to relate user registration flow", ex);
        }
    }

    private async Task<UserLinkRegistrationDataResponse?> PostRelateUserRegistrationFlow(string identityUserId)
    {
        var response = await HttpClient.PostAsync(
            requestUri: "Auth/RelateUserRegistrationFlow",
            content: new StringContent(
                content: JsonSerializer.Serialize(identityUserId),
                encoding: Encoding.UTF8,
                mediaType: "application/json"
            ));
        
        Logger.LogDebug("Response http code from linking user with identity user id: " +
                        $"{identityUserId} to app with name: {AppLinkingName} is: {response.StatusCode}");
        
        if (!response.IsSuccessStatusCode)
        {
            Logger.LogError("Failed to relate user registration flow." +
                            $"Response {JsonSerializer.Serialize(response)}");
            throw new CognitoPostAuthenticationException("Failed to relate user registration flow");
        }
        
        await using var contentStream = await response.Content.ReadAsStreamAsync();
        var userLinkRegistrationData = JsonSerializer.Deserialize<UserLinkRegistrationDataResponse>(contentStream);
            
        Logger.LogDebug("Successfully got a response from a relating API." +
                        $"Response {JsonSerializer.Serialize(userLinkRegistrationData)}");

        return userLinkRegistrationData;
    }

    private bool AppUserIdLinked()
    {
        string? applicationUserIdValue = null;
        var containsApplicationUserId = TriggerEvent.Request?.UserAttributes?
            .TryGetValue(ApplicationUserIdName, out applicationUserIdValue);

        if (containsApplicationUserId is null || !containsApplicationUserId.Value)
        {
            throw new InvalidOperationException(
                $"Field {nameof(containsApplicationUserId)} is not accessible in user attributes." +
                "Check if UserAttributes exists in request.");
        }

        return !string.IsNullOrEmpty(applicationUserIdValue);
    }
}
