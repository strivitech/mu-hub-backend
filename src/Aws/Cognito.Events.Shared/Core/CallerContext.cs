using System.Text.Json.Serialization;

namespace Cognito.Events.Shared.Core;

public class CallerContext
{
    [JsonPropertyName("awsSdkVersion")]
    public string? AwsSdkVersion { get; set; }

    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }
}
