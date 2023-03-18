namespace MuHub.Market.Proxy.Features.Coins.Mapping;

/// <summary>
/// To market coin dto mapper.
/// </summary>
internal static class ToMarketCoinMapper
{
    /// <summary>
    /// Maps from <see cref="CoinGecko.Api.Features.Coins.MarketCoin"/> to <see cref="MarketCoinDto"/>.
    /// </summary>
    /// <param name="coin">Gecko market coin.</param>
    /// <returns>An instance of <see cref="MarketCoinDto"/>.</returns>
    public static MarketCoinDto ToMarketCoinDto(this CoinGecko.Api.Features.Coins.MarketCoin coin) =>
        coin.CreateMarketCoinDto(coin.ValidateCoin());
    
    /// <summary>
    /// Maps from a enumerable of <see cref="CoinGecko.Api.Features.Coins.MarketCoin"/> to a list of <see cref="MarketCoinDto"/>.
    /// </summary>
    /// <param name="coins">Gecko market coins.</param>
    /// <returns>A list of <see cref="MarketCoinDto"/>.</returns>
    public static List<MarketCoinDto> ToMarketCoinDtoList(
        this IEnumerable<CoinGecko.Api.Features.Coins.MarketCoin> coins) =>
        coins.Select(ToMarketCoinDto).ToList();

    private static MarketCoinDto CreateMarketCoinDto(this CoinGecko.Api.Features.Coins.MarketCoin coin, bool isValid) =>
        new(Id: coin.Id!,
            CurrentPrice: coin.CurrentPrice,
            MarketCapRank: coin.MarketCapRank,
            LastUpdated: coin.LastUpdated,
            ImageUrl: coin.Image,
            High24H: coin.High24H,
            Low24H: coin.Low24H,
            IsValid: isValid);
}
