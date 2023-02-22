using MuHub.Domain.Common.Aggregates;
using MuHub.Domain.Common.Entities;

namespace MuHub.Domain.Entities;

public class MarketCoin : BaseEntity<int>, IAggregateRoot
{
    public string SymbolId { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }
    
    public decimal CurrentPrice { get; set; } 

    public decimal? High24H { get; set; }

    public decimal? Low24H { get; set; }
    
    public DateTimeOffset LastUpdated { get; set; }
}
