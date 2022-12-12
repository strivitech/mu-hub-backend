using System.Text.Json.Serialization;

using Cognito.Events.Shared.Core;

namespace Cognito.Events.Shared.Events;

public class PostConfirmationRequest : RequestBase
{
    [JsonPropertyName("clientMetadata")]
    public Dictionary<string, string>? ClientMetadata { get; set; }
}