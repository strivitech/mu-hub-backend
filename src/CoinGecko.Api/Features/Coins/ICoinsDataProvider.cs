using FluentResults;

namespace CoinGecko.Api.Features.Coins;

/// <summary>
/// Coins data provider.
/// </summary>
public interface ICoinsDataProvider
{
    /// <summary>
    /// Asynchronously gets the coin list.
    /// </summary>
    /// <param name="includePlatform">Flag determines whether should include platform or not.</param>
    /// <returns>A <see cref="Result{TValue}"/> of coins.</returns>
    Task<Result<List<Coin>>> GetCoinListAsync(bool includePlatform = false);

    /// <summary>
    /// Asynchronously gets the market coin list.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>A <see cref="Result{TValue}"/> of coins.</returns>
    Task<Result<List<MarketCoin>>> GetMarketCoinListAsync(GetMarketCoinRequest request);
}
