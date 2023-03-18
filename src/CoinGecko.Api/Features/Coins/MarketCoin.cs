using System.Text.Json.Serialization;

namespace CoinGecko.Api.Features.Coins;

/// <summary>
/// Market Coin.
/// </summary>
public class MarketCoin
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
    /// Image.
    /// </summary>
    [JsonPropertyName("image")] 
    public string? Image { get; set; }

    /// <summary>
    /// Current Price.
    /// </summary>
    [JsonPropertyName("current_price")] 
    public decimal? CurrentPrice { get; set; }

    /// <summary>
    /// Market Cap.
    /// </summary>
    [JsonPropertyName("market_cap")] 
    public decimal? MarketCap { get; set; }
    
    /// <summary>
    /// Market Cap Rank.
    /// </summary>
    [JsonPropertyName("market_cap_rank")]
    public int? MarketCapRank { get; set; }

    /// <summary>
    /// Total Volume.
    /// </summary>
    [JsonPropertyName("total_volume")] 
    public decimal? TotalVolume { get; set; }

    /// <summary>
    /// High 24H value.
    /// </summary>
    [JsonPropertyName("high_24h")] 
    public decimal? High24H { get; set; }

    /// <summary>
    /// Low 24H value.
    /// </summary>
    [JsonPropertyName("low_24h")] 
    public decimal? Low24H { get; set; }

    /// <summary>
    /// All Time High.
    /// </summary>
    [JsonPropertyName("ath")] 
    public decimal? Ath { get; set; }

    /// <summary>
    /// All Time High Change Percentage.
    /// </summary>
    [JsonPropertyName("ath_change_percentage")]
    public decimal? AthChangePercentage { get; set; }

    /// <summary>
    /// All Time High Date.
    /// </summary>
    [JsonPropertyName("ath_date")] 
    public DateTimeOffset? AthDate { get; set; }

    /// <summary>
    /// Roi.
    /// </summary>
    [JsonPropertyName("roi")] 
    public Roi? Roi { get; set; }

    /// <summary>
    /// Last Updated.
    /// </summary>
    [JsonPropertyName("last_updated")] 
    public DateTimeOffset? LastUpdated { get; set; }
}
