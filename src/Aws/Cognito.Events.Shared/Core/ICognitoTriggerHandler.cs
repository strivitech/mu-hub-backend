using System.Text.Json;

namespace Cognito.Events.Shared.Core;

public interface ICognitoTriggerHandler
{
    string? TriggerSource { get; }

    JsonElement HandleTriggerEvent();
    
    Task<JsonElement> HandleTriggerEventAsync();
}