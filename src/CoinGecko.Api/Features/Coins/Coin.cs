using System.Text.Json.Serialization;

namespace CoinGecko.Api.Features.Coins;

public class Coin
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("platforms")]
    public Dictionary<string,string>? Platforms { get; set; }
}
