using System.Text.Json.Serialization;

namespace Cognito.Events.Shared.Core;

public class CognitoTriggerEventBase : ICognitoTriggerEvent 
{
    [JsonPropertyName("version")]
    public string Version { get; set; } = null!;

    [JsonPropertyName("triggerSource")]
    public string TriggerSource { get; set; } = null!;

    [JsonPropertyName("region")]
    public string Region { get; set; } = null!;

    [JsonPropertyName("userPoolId")]
    public string UserPoolId { get; set; } = null!;
    
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = null!;

    [JsonPropertyName("callerContext")]
    public CallerContext CallerContext { get; set; } = null!;
}
