using Microsoft.EntityFrameworkCore;

using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Application.Contracts.Persistence;

public interface IApplicationDbContext : IApplicationDbContextInstanceResolver
{
    DbSet<Coin> Coins { get; }
    
    DbSet<MarketCoin> MarketCoins { get; }
    
    DbSet<MarketCoinsUpdateTimestamp> MarketCoinsUpdateTimestamps { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
