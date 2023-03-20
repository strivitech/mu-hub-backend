using System.Text.Json.Serialization;

namespace CoinGecko.Api.Features.Coins;

/// <summary>
/// Represents the return on investment (ROI) of a coin.
/// </summary>
public class Roi
{
    /// <summary>
    /// The times of the ROI.
    /// </summary>
    [JsonPropertyName("times")] 
    public decimal? Times { get; set; }

    /// <summary>
    /// Currency of the ROI.
    /// </summary>
    [JsonPropertyName("currency")] 
    public string? Currency { get; set; }

    /// <summary>
    /// Percentage of the ROI.
    /// </summary>
    [JsonPropertyName("percentage")] 
    public decimal? Percentage { get; set; }
}
