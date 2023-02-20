using MuHub.Application.Structures;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Application.Contracts.Persistence;

public interface ICoinsStorage
{
    Task<List<Coin>> GetAllCoinsAsync();
    
    Task<List<Coin>?> GetCoinsBySymbolAsync(string symbol);
    
    Task<PagedList<Coin>?> GetCoinsAsync(int page, int pageSize);
    
    Task AddCoinAsync(Coin coin);
    
    Task AddCoinsAsync(IEnumerable<Coin> coins);
}
