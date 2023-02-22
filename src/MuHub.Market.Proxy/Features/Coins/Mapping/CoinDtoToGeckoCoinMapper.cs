namespace MuHub.Market.Proxy.Features.Coins.Mapping;

/// <summary>
/// Mapping from CoinGecko coin to coin DTO.
/// </summary>
internal static class CoinDtoToGeckoCoinMapper
{
    /// <summary>
    /// Maps from CoinGecko coin to MuHub coin.
    /// </summary>
    /// <param name="coin">Coin.</param>
    /// <returns><see cref="CoinDto"/>.</returns>
    public static CoinDto ToCoinDto(this CoinGecko.Api.Features.Coins.Coin coin)
    {
        return coin.ValidateCoin()
            ? coin.CreateCoinDto()
            : throw new ArgumentException($"Coin is not valid. CoinId: {coin.Id}");
    }

    public static MarketCoinDto ToMarketCoinDto(this CoinGecko.Api.Features.Coins.MarketCoin coin)
    {
        return coin.ValidateCoin()
            ? coin.CreateMarketCoinDto()
            : throw new ArgumentException($"MarketCoin is not valid. MarketCoinId: {coin.Id}");
    }

    /// <summary>
    /// Maps from CoinGecko coins list to MuHub coins list.
    /// </summary>
    /// <param name="coins">Coins.</param>
    /// <returns><see cref="List{T}"/> of <see cref="CoinDto"/>.</returns>
    public static List<CoinDto> ToCoinDtoList(this IEnumerable<CoinGecko.Api.Features.Coins.Coin> coins) =>
        coins.Select(ToCoinDto).ToList();

    public static List<MarketCoinDto> ToMarketCoinDtoList(this IEnumerable<CoinGecko.Api.Features.Coins.MarketCoin> coins) =>
        coins.Select(ToMarketCoinDto).ToList();
    
    private static CoinDto CreateCoinDto(this CoinGecko.Api.Features.Coins.Coin coin) =>
        new(coin.Id!, coin.Symbol!, coin.Name!);

    private static MarketCoinDto CreateMarketCoinDto(this CoinGecko.Api.Features.Coins.MarketCoin coin) =>
        new(coin.Id!, coin.Symbol!, coin.Name!, coin.CurrentPrice!.Value, coin.LastUpdated!.Value, coin.Image,
            coin.High24H, coin.Low24H);
}
