﻿using Microsoft.EntityFrameworkCore;

using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Structures;
using MuHub.Domain.Entities;

namespace MuHub.Infrastructure.Persistence;

/// <summary>
/// Coins storage.
/// </summary>
public class CoinsStorage : ICoinsStorage
{
    private readonly IApplicationDbContext _dbContext;

    public CoinsStorage(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Coin>> GetAllAsync() => await _dbContext.Coins.ToListAsync();

    public async Task<Dictionary<string, Coin>> GetAllDictionaryByExternalSymbolIdAsync() =>
        await _dbContext.Coins.ToDictionaryAsync(x => x.ExternalSymbolId);

    public async Task<List<Coin>?> GetBySymbolAsync(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
        {
            return null;
        }

        return await _dbContext.Coins.Where(c => c.Symbol == symbol).ToListAsync();
    }

    public async Task<PagedList<Coin>?> GetPagedAsync(int page, int pageSize)
    {
        if (page < 1 || pageSize < 1)
        {
            return null;
        }

        var query = _dbContext.Coins.Skip((page - 1) * pageSize).Take(pageSize);
        return await PagedList<Coin>.CreateAsync(query, page, pageSize);
    }

    public async Task AddAsync(Coin coin)
    {
        _dbContext.Coins.Add(coin);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAsync(IEnumerable<Coin> coins)
    {
        _dbContext.Coins.AddRange(coins);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Coin coin)
    {
        _dbContext.Coins.Update(coin);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(IEnumerable<Coin> coins)
    {
        _dbContext.Coins.UpdateRange(coins);
        await _dbContext.SaveChangesAsync();
    }
}
