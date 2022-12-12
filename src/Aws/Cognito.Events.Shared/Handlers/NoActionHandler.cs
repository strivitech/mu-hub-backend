using System.Text.Json;

using Amazon.Lambda.Core;

using Cognito.Events.Shared.Core;

namespace Cognito.Events.Shared.Handlers;

internal class NoActionHandler : CognitoTriggerHandler<CognitoTriggerEventBase>
{
    public override string TriggerSource => string.Empty;
    
    public NoActionHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }

    public override async Task<JsonElement> HandleTriggerEventAsync()
    {
        Logger.LogInformation("No action handler");

        return await base.HandleTriggerEventAsync();
    }
}