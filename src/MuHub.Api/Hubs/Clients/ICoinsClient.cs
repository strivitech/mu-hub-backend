using MuHub.Api.Responses;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Api.Hubs.Clients;

/// <summary>
/// Coins client interface.
/// </summary>
public interface ICoinsClient
{
    /// <summary>
    /// Gets coins.
    /// </summary>
    /// <returns></returns>
    Task UpdateCoinsInformation(IEnumerable<MarketCoinResponse> coins);
}
