using CoinGecko.Api.Features.Coins;

namespace MuHub.Market.Proxy.Features.Coins;

public static class CoinValidators
{
    public static bool ValidateCoin(this Coin coin)
    {
        return ValidateCoinId(coin.Id) && ValidateCoinSymbol(coin.Symbol) && ValidateCoinName(coin.Name);
    }

    public static bool ValidateCoin(this MarketCoin coin)
    {
        return ValidateCoinId(coin.Id) && ValidateCoinSymbol(coin.Symbol) && ValidateCoinName(coin.Name) &&
               ValidateCurrentPrice(coin.CurrentPrice) && ValidateLastUpdated(coin.LastUpdated);
    }

    /// <summary>
    /// Validates coin id.
    /// </summary>
    /// <param name="id">Coin id.</param>
    /// <returns><see cref="bool"/>.</returns>
    private static bool ValidateCoinId(this string? id) => !string.IsNullOrWhiteSpace(id);

    /// <summary>
    /// Validates coin symbol.
    /// </summary>
    /// <param name="symbol">Coin symbol.</param>
    /// <returns><see cref="bool"/>.</returns>
    private static bool ValidateCoinSymbol(this string? symbol) => !string.IsNullOrWhiteSpace(symbol);

    /// <summary>
    /// Validates coin name.
    /// </summary>
    /// <param name="name">Coin name.</param>
    /// <returns><see cref="bool"/>.</returns>
    private static bool ValidateCoinName(this string? name) => !string.IsNullOrWhiteSpace(name);

    private static bool ValidateCurrentPrice(this decimal? currentPrice) => currentPrice is > 0;

    private static bool ValidateLastUpdated(this DateTimeOffset? lastUpdated) => lastUpdated is not null;
}
