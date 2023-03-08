using MuHub.Application.Structures;
using MuHub.Domain.Entities;

namespace MuHub.Application.Contracts.Persistence;

public interface ICoinsStorage
{
    Task<List<Coin>> GetAllAsync();

    Task<Dictionary<string, Coin>> GetAllDictionaryByExternalSymbolIdAsync();

    Task<List<Coin>?> GetBySymbolAsync(string symbol);

    Task<PagedList<Coin>?> GetPagedAsync(int page, int pageSize);

    Task AddAsync(Coin coin);

    Task AddAsync(IEnumerable<Coin> coins);
    
    Task UpdateAsync(Coin coin);
    
    Task UpdateAsync(IEnumerable<Coin> coins);
}
