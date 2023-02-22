using System.Text.Json.Serialization;

namespace CoinGecko.Api.Features.Coins;

/// <summary>
/// Coin.
/// </summary>
public class Coin
{
    /// <summary>
    /// Id.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Symbol.
    /// </summary>
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Platforms.
    /// </summary>
    [JsonPropertyName("platforms")]
    public Dictionary<string,string>? Platforms { get; set; }
}
