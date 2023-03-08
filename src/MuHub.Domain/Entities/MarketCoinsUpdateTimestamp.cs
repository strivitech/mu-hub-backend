using MuHub.Domain.Common.Aggregates;
using MuHub.Domain.Common.Entities;

namespace MuHub.Domain.Entities;

public class MarketCoinsUpdateTimestamp : BaseEntity<long>
{
    public DateTimeOffset LastUpdated { get; set; }
}
