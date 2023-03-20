using FluentResults;

namespace MuHub.Market.Proxy.Features.Coins;

/// <summary>
/// Coins data service.
/// </summary>
public interface ICoinsDataService
{
    /// <summary>
    /// Asynchronously get market coin list.
    /// </summary>
    /// <param name="request">Get market coin request.</param>
    /// <returns>A <see cref="Result"/> of market coin list.</returns>
    Task<Result<List<MarketCoinDto>>> GetMarketCoinListAsync(GetMarketCoinRequest request);
}
