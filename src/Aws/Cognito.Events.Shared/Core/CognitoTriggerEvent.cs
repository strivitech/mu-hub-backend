using System.Text.Json.Serialization;

namespace Cognito.Events.Shared.Core;

public abstract class CognitoTriggerEvent<TRequest, TResponse> : CognitoTriggerEventBase 
    where TRequest : RequestBase
{
    [JsonPropertyName("request")]
    public TRequest? Request { get; set; }

    [JsonPropertyName("response")]
    public TResponse? Response { get; set; }
}