using FluentResults;

namespace MuHub.Market.Proxy.Features.Coins;

/// <summary>
/// Coins data service.
/// </summary>
public interface ICoinsDataService
{
    Task<Result<List<MarketCoinDto>>> GetMarketCoinListAsync(GetMarketCoinRequest request);
}
