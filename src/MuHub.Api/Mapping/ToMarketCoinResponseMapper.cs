using MuHub.Api.Responses;
using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Api.Mapping;

public static class ToMarketCoinResponseMapper
{
    public static IEnumerable<MarketCoinResponse> ToMarketCoinResponseEnumerable(
        this IEnumerable<MarketCoin> marketCoins) =>
        marketCoins
            .OrderBy(x => x.MarketCapRank)
            .Select(ToMarketCoinResponse);

    public static MarketCoinResponse ToMarketCoinResponse(this MarketCoin marketCoin)
    {
        return new MarketCoinResponse(
            marketCoin.SymbolId,
            marketCoin.Name,
            marketCoin.Symbol,
            marketCoin.CurrentPrice,
            marketCoin.LastUpdated,
            marketCoin.ImageUrl,
            marketCoin.High24H,
            marketCoin.Low24H,
            marketCoin.IsValid);
    }
}
