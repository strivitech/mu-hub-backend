using System.Text.Json.Serialization;

using Cognito.Events.Shared.Core;

namespace Cognito.Events.Shared.Events;

public class PostAuthenticationRequest : RequestBase
{
    [JsonPropertyName("validationData")]
    public Dictionary<string, string>? ValidationData { get; set; }

    [JsonPropertyName("userNotFound")]
    public bool UserNotFound { get; set; }
}
