using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;

using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Structures;
using MuHub.Domain.Entities;

namespace MuHub.Infrastructure.Persistence;

public class MarketCoinStorage : IMarketCoinStorage
{
    private readonly IApplicationDbContext _dbContext;

    public MarketCoinStorage(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<MarketCoin>> GetAllMarketCoinsAsync() => await _dbContext.MarketCoins.ToListAsync();

    public async Task<List<MarketCoin>?> GetMarketCoinsBySymbolAsync(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
        {
            return null;
        }
        
        return await _dbContext.MarketCoins.Where(c => c.Symbol == symbol).ToListAsync();
    }

    public async Task<PagedList<MarketCoin>?> GetMarketCoinsAsync(int page, int pageSize)
    {
        if (page < 1 || pageSize < 1)
        {
            return null;
        }

        var query = _dbContext.MarketCoins.Skip((page - 1) * pageSize).Take(pageSize);
        return await PagedList<MarketCoin>.CreateAsync(query, page, pageSize);
    }

    public async Task AddMarketCoinAsync(MarketCoin coin)
    {
        await _dbContext.MarketCoins.AddAsync(coin);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddMarketCoinsAsync(IEnumerable<MarketCoin> coins)
    {
        await _dbContext.MarketCoins.AddRangeAsync(coins);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveMarketCoinAsync(MarketCoin coin)
    {
        _dbContext.MarketCoins.Remove(coin);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveMarketCoinsAsync(IEnumerable<MarketCoin> coins)
    {
        _dbContext.MarketCoins.RemoveRange(coins);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAllMarketCoinsAsync()
    {
        await _dbContext.Instance.TruncateAsync<MarketCoin>();
    }

    public async Task ReplaceAllMarketCoinsAsync(IEnumerable<MarketCoin> coins)
    {
        var transaction = await _dbContext.Instance.Database.BeginTransactionAsync();
        try
        {
            await RemoveAllMarketCoinsAsync();
            await AddMarketCoinsAsync(coins);
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
