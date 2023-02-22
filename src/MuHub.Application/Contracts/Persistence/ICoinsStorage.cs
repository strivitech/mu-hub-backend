using MuHub.Application.Structures;
using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Application.Contracts.Persistence;

public interface ICoinsStorage
{
    Task<List<Coin>> GetAllCoinsAsync();
    
    Task<List<Coin>?> GetCoinsBySymbolAsync(string symbol);
    
    Task<PagedList<Coin>?> GetCoinsAsync(int page, int pageSize);
    
    Task AddCoinAsync(Coin coinDto);
    
    Task AddCoinsAsync(IEnumerable<Coin> coins);
    
    Task RemoveCoinAsync(Coin coinDto);
    
    Task RemoveCoinsAsync(IEnumerable<Coin> coins);
    
    Task RemoveAllCoinsAsync();

    Task ReplaceAllCoinsAsync(IEnumerable<Coin> coins);
}
