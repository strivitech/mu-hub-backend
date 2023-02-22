using CoinGecko.Api.Features.Coins;

namespace MuHub.Market.Proxy.Features.Coins;

internal static class CoinRequestMapper
{
    public static CoinGecko.Api.Features.Coins.GetMarketCoinRequest MapFromGetMarketCoinRequest(this GetMarketCoinRequest request)
    {
        return new CoinGecko.Api.Features.Coins.GetMarketCoinRequest
        {
            Ids = request.Ids is null ? null : string.Join(",", request.Ids),
            PerPage = request.PerPage,
            Page = request.Page,
            VsCurrency = RetrieveCurrency(request.Currency),
            Order = RetrieveOrder(request.Order)
        };
    }

    private static VsCurrency RetrieveCurrency(Currency currency)
    {
        return currency switch
        {
            Currency.Usd => VsCurrency.Usd,
            Currency.Eur => VsCurrency.Eur,
            _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, "Currency not supported.")
        };
    }
    
    private static CoinGecko.Api.Features.Coins.Order RetrieveOrder(Order order)
    {
        return order switch
        {
            Order.MarketCapDesc => CoinGecko.Api.Features.Coins.Order.MarketCapDesc,
            Order.MarketCapAsc => CoinGecko.Api.Features.Coins.Order.MarketCapAsc,
            _ => throw new ArgumentOutOfRangeException(nameof(order), order, "Order not supported.")
        };
    }
}
