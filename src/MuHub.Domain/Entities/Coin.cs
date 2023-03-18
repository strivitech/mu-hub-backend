using MuHub.Domain.Common.Aggregates;
using MuHub.Domain.Common.Entities;

namespace MuHub.Domain.Entities;

/// <summary>
/// Represents a coin.
/// </summary>
public class Coin : BaseEntity<int>
{
    /// <summary>
    /// External symbol id.
    /// </summary>
    public string ExternalSymbolId { get; set; } = null!;
    
    /// <summary>
    /// Symbol id.
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
}
