using System.Text.Json.Serialization;

namespace Cognito.Events.Shared.Core;

public abstract class RequestBase
{
    [JsonPropertyName("userAttributes")]
    public Dictionary<string, string> UserAttributes { get; set; } = new();
}
