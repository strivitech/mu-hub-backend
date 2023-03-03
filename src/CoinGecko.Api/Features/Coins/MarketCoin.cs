using System.Text.Json.Serialization;

namespace CoinGecko.Api.Features.Coins;

public class MarketCoin
{
    [JsonPropertyName("id")] 
    public string? Id { get; set; }

    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    [JsonPropertyName("name")] 
    public string? Name { get; set; }

    [JsonPropertyName("image")] 
    public string? Image { get; set; }

    [JsonPropertyName("current_price")] 
    public decimal? CurrentPrice { get; set; }

    [JsonPropertyName("market_cap")] 
    public decimal? MarketCap { get; set; }
    
    [JsonPropertyName("market_cap_rank")]
    public int? MarketCapRank { get; set; }

    [JsonPropertyName("total_volume")] 
    public decimal? TotalVolume { get; set; }

    [JsonPropertyName("high_24h")] 
    public decimal? High24H { get; set; }

    [JsonPropertyName("low_24h")] 
    public decimal? Low24H { get; set; }

    [JsonPropertyName("ath")] 
    public decimal? Ath { get; set; }

    [JsonPropertyName("ath_change_percentage")]
    public decimal? AthChangePercentage { get; set; }

    [JsonPropertyName("ath_date")] 
    public DateTimeOffset? AthDate { get; set; }

    [JsonPropertyName("roi")] 
    public Roi? Roi { get; set; }

    [JsonPropertyName("last_updated")] 
    public DateTimeOffset? LastUpdated { get; set; }
}
