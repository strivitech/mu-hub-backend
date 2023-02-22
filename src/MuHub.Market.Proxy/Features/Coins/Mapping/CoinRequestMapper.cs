using CoinGecko.Api.Features.Coins;

namespace MuHub.Market.Proxy.Features.Coins.Mapping;

internal static class CoinRequestMapper
{
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
