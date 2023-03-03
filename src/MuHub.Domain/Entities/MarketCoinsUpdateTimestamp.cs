using MuHub.Domain.Common.Aggregates;
using MuHub.Domain.Common.Entities;

namespace MuHub.Domain.Entities;

public class MarketCoinsUpdateTimestamp : BaseEntity<int>, IAggregateRoot
{
    public DateTimeOffset LastUpdated { get; set; }
}
