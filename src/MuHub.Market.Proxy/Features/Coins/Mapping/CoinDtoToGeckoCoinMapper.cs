namespace MuHub.Market.Proxy.Features.Coins.Mapping;

/// <summary>
/// Mapping from CoinGecko coin to coin DTO.
/// </summary>
internal static class CoinDtoToGeckoCoinMapper
{
    public static MarketCoinDto ToMarketCoinDto(this CoinGecko.Api.Features.Coins.MarketCoin coin) =>
        coin.CreateMarketCoinDto(coin.ValidateCoin());
    
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
