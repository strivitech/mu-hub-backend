using MuHub.Api.Responses;
using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Api.Mapping;

/// <summary>
/// This class is used to map an instance to a <see cref="MarketCoinResponse"/>.
/// </summary>
public static class ToMarketCoinResponseMapper
{
    /// <summary>
    /// This method is used to map a enumerable of <see cref="MarketCoin"/> to enumerable of <see cref="MarketCoinResponse"/>.
    /// </summary>
    /// <param name="marketCoins">Market coins.</param>
    public static IEnumerable<MarketCoinResponse> ToMarketCoinResponseEnumerable(
        this IEnumerable<MarketCoin> marketCoins) =>
        marketCoins
            .OrderBy(x => x.MarketCapRank)
            .Select(ToMarketCoinResponse);

    /// <summary>
    /// This method is used to map <see cref="MarketCoin"/> to a <see cref="MarketCoinResponse"/>.
    /// </summary>
    /// <param name="marketCoin">Market coin.</param>
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
