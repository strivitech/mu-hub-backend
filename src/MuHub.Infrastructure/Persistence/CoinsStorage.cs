using Microsoft.EntityFrameworkCore;

using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Structures;
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
}
