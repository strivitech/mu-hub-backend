using MuHub.Domain.Common.Aggregates;
using MuHub.Domain.Common.Entities;

namespace MuHub.Domain.Entities;

public class Coin : BaseEntity<int>
{
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
