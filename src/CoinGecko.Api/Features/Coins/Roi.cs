using System.Text.Json.Serialization;

namespace CoinGecko.Api.Features.Coins;

public class Roi
{
    [JsonPropertyName("times")] 
    public decimal? Times { get; set; }

    [JsonPropertyName("currency")] 
    public string? Currency { get; set; }

    [JsonPropertyName("percentage")] 
    public decimal? Percentage { get; set; }
}
