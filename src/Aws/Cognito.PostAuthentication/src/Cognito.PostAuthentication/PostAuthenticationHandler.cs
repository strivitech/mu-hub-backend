using System.Net;
using System.Text;
using System.Text.Json;

using Amazon.CognitoIdentityProvider.Model;
using Amazon.Lambda.Core;

using Cognito.Common;
using Cognito.Events.Shared.Core;
using Cognito.Events.Shared.Events;
using Cognito.Events.Shared.Exceptions;

namespace Cognito.PostAuthentication;

public class PostAuthenticationHandler : CognitoTriggerHandler<PostAuthenticationEvent>
{
    public override string TriggerSource => Config.TriggerSourceName;

    public PostAuthenticationHandler(
        JsonElement cognitoEvent,
        ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }

    public override async Task<JsonElement> HandleTriggerEventAsync()
    {
        if (!AppUserIdLinked())
        {
            throw new CognitoPostAuthenticationException("App user id not linked");
        }
        
        return await base.HandleTriggerEventAsync();
    }

    private bool AppUserIdLinked()
    {
        var containsApplicationUserId =
            TryRetrieveUserAttribute(UserAttributesNames.ApplicationLinked, out string? applicationLinked);

        if (!containsApplicationUserId)
        {
            throw new InvalidOperationException(
                $"Field {nameof(containsApplicationUserId)} is not accessible in the user attributes");
        }

        return applicationLinked.IsCognitoApplicationLinked();
    }

    private bool TryRetrieveUserAttribute(string key, out string? value)
    {
        var isSuccess = TriggerEvent.Request.UserAttributes.TryGetValue(key, out string? val);
        value = val;
        return isSuccess;
    }
}
