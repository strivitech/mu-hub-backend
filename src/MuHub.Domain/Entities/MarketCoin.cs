using MuHub.Domain.Common.Aggregates;
using MuHub.Domain.Common.Entities;

namespace MuHub.Domain.Entities;

/// <summary>
/// Represents a market coin.
/// </summary>
public class MarketCoin : BaseEntity<int>
{
    /// <summary>
    /// Symbol ID.
    /// </summary>
    public string SymbolId { get; set; } = null!;

    /// <summary>
    /// Symbol.
    /// </summary>
    public string Symbol { get; set; } = null!;

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Image URL.
    /// </summary>
    public string? ImageUrl { get; set; }
    
    /// <summary>
    /// Current price.
    /// </summary>
    public decimal? CurrentPrice { get; set; } 
    
    /// <summary>
    /// Market cap.
    /// </summary>
    public int? MarketCapRank { get; set; }

    /// <summary>
    /// High 24H value.
    /// </summary>
    public decimal? High24H { get; set; }

    /// <summary>
    /// Low 24H value.
    /// </summary>
    public decimal? Low24H { get; set; }
    
    /// <summary>
    /// Last updated.
    /// </summary>
    public DateTimeOffset? LastUpdated { get; set; }
    
    /// <summary>
    /// Determines if the coin is valid for interaction.
    /// </summary>
    public bool IsValid { get; set; }
}
