using System.Reflection;

using Microsoft.EntityFrameworkCore;

using MuHub.Application.Contracts.Persistence;
using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbContext Instance => this;

    public DbSet<Coin> Coins => Set<Coin>();
    public DbSet<MarketCoin> MarketCoins => Set<MarketCoin>();
    public DbSet<MarketCoinsUpdateTimestamp> MarketCoinsUpdateTimestamps => Set<MarketCoinsUpdateTimestamp>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Here custom logic

        return await base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Here custom logic
    }
}
