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
    // TODO: Think about paginating while sending new coins info.
    Task UpdateCoinsInformation(IEnumerable<MarketCoinDto> coins);
}
