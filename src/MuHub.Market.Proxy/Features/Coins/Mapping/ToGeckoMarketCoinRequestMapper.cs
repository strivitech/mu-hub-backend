using CoinGecko.Api.Features.Coins;

namespace MuHub.Market.Proxy.Features.Coins.Mapping;

/// <summary>
/// To Gecko market coin request mapper.
/// </summary>
internal static class ToGeckoMarketCoinRequestMapper
{
    /// <summary>
    /// Maps from <see cref="GetMarketCoinRequest"/> to <see cref="CoinGecko.Api.Features.Coins.GetMarketCoinRequest"/>.
    /// </summary>
    /// <param name="request">Get market coin request.</param>
    /// <returns>A <see cref="CoinGecko.Api.Features.Coins.GetMarketCoinRequest"/>.</returns>
    public static CoinGecko.Api.Features.Coins.GetMarketCoinRequest MapFromGetMarketCoinRequest(this GetMarketCoinRequest request)
    {
        return new CoinGecko.Api.Features.Coins.GetMarketCoinRequest
        {
            Ids = request.Ids is null ? null : string.Join(",", request.Ids),
            PerPage = request.PerPage,
            Page = request.Page,
            VsCurrency = request.Currency.Value,
            Order = request.Order.Value
        };
    }
}
