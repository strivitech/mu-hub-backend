using Microsoft.EntityFrameworkCore;

using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Application.Contracts.Persistence;

/// <summary>
/// Application database context interface.
/// </summary>
public interface IApplicationDbContext : IApplicationDbContextInstanceResolver
{
    /// <summary>
    /// Coins.
    /// </summary>
    DbSet<Coin> Coins { get; }
    
    /// <summary>
    /// Market coins.
    /// </summary>
    DbSet<MarketCoin> MarketCoins { get; }

    /// <summary>
    /// Market coins update timestamps.
    /// </summary>
    DbSet<MarketCoinsUpdateTimestamp> MarketCoinsUpdateTimestamps { get; }
    
    DbSet<WatchList> WatchList { get; }

    /// <summary>
    /// Asynchronously saves changes to the database.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
