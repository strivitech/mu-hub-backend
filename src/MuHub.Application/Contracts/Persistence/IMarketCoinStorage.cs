using MuHub.Application.Structures;
using MuHub.Domain.Entities;

namespace MuHub.Application.Contracts.Persistence;

public interface IMarketCoinStorage
{
    Task<List<MarketCoin>> GetAllMarketCoinsAsync();
    
    Task<List<MarketCoin>?> GetMarketCoinsBySymbolAsync(string symbol);
    
    Task<PagedList<MarketCoin>?> GetMarketCoinsAsync(int page, int pageSize);
    
    Task AddMarketCoinAsync(MarketCoin marketCoinDto);
    
    Task AddMarketCoinsAsync(IEnumerable<MarketCoin> marketCoins);
    
    Task RemoveMarketCoinAsync(MarketCoin marketCoinDto);
    
    Task RemoveMarketCoinsAsync(IEnumerable<MarketCoin> marketCoins);
    
    Task RemoveAllMarketCoinsAsync();
    
    Task ReplaceAllMarketCoinsAsync(IEnumerable<MarketCoin> marketCoins);
}
