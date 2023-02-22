using Microsoft.EntityFrameworkCore;

using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Structures;
using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Infrastructure.Persistence;

public class CoinsStorage : ICoinsStorage
{
    private readonly IApplicationDbContext _dbContext;

    public CoinsStorage(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Coin>> GetAllCoinsAsync() => await _dbContext.Coins.ToListAsync();

    public async Task<List<Coin>?> GetCoinsBySymbolAsync(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
        {
            return null;
        }
        
        return await _dbContext.Coins.Where(c => c.Symbol == symbol).ToListAsync();
    }

    public async Task<PagedList<Coin>?> GetCoinsAsync(int page, int pageSize)
    {
        if (page < 1 || pageSize < 1)
        {
            return null;
        }

        var query = _dbContext.Coins.Skip((page - 1) * pageSize).Take(pageSize);
        return await PagedList<Coin>.CreateAsync(query, page, pageSize);
    }

    public async Task AddCoinAsync(Coin coin)
    {
        await _dbContext.Coins.AddAsync(coin);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddCoinsAsync(IEnumerable<Coin> coins)
    {
        await _dbContext.Coins.AddRangeAsync(coins);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveCoinAsync(Coin coin)
    {
        _dbContext.Coins.Remove(coin);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveCoinsAsync(IEnumerable<Coin> coins)
    {
        _dbContext.Coins.RemoveRange(coins);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAllCoinsAsync()
    {
        // await _dbContext.Instance.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {nameof(_dbContext.Coins)}");
        var coins = await _dbContext.Coins.ToListAsync();
        _dbContext.Coins.RemoveRange(coins);
        await _dbContext.SaveChangesAsync();
    }

    public async Task ReplaceAllCoinsAsync(IEnumerable<Coin> coins)
    {
        var transaction = await _dbContext.Instance.Database.BeginTransactionAsync();
        try
        {
            await RemoveAllCoinsAsync();
            await AddCoinsAsync(coins);
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
