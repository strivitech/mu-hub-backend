using FluentResults;

namespace MuHub.Market.Proxy.Features.Coins;

/// <summary>
/// Coins data service.
/// </summary>
public interface ICoinsDataService
{
    /// <summary>
    /// Asynchronously gets the coin list.
    /// </summary>
    /// <returns>A <see cref="Result"/> of <see cref="List{T}"/> of <see cref="CoinDto"/>.</returns>
    Task<Result<List<CoinDto>>> GetCoinListAsync();
    
    Task<Result<List<MarketCoinDto>>> GetMarketCoinListAsync(GetMarketCoinRequest request);
}
