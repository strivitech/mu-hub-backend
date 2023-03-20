using MuHub.Domain.Common.Aggregates;
using MuHub.Domain.Common.Entities;

namespace MuHub.Domain.Entities;

/// <summary>
/// Represents the last time the market coins were updated.
/// </summary>
public class MarketCoinsUpdateTimestamp : BaseEntity<long>
{
    /// <summary>
    /// Last time the market coins were updated.
    /// </summary>
    public DateTimeOffset LastUpdated { get; set; }
}
