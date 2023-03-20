using CoinGecko.Api.Features.Coins;

namespace MuHub.Market.Proxy.Features.Coins;

/// <summary>
/// Contains coin validators.
/// </summary>
public static class CoinValidators
{
    /// <summary>
    /// Validates market coin.
    /// </summary>
    /// <param name="coin">Market coin.</param>
    /// <returns>True if coin is valid, otherwise false.</returns>
    /// <exception cref="InvalidOperationException">When coin id is invalid.</exception>
    public static bool ValidateCoin(this MarketCoin coin)
    {
        if (!ValidateCoinId(coin.Id))
        {
            throw new InvalidOperationException("Coin id is invalid. Check if the coin id is not null or empty.");
        }

        return ValidateMarketCapRank(coin.MarketCapRank) && ValidateCurrentPrice(coin.CurrentPrice) &&
               ValidateLastUpdated(coin.LastUpdated);
    }
    
    private static bool ValidateMarketCapRank(this int? capRank) => capRank is > 0;

    private static bool ValidateCoinId(this string? id) => !string.IsNullOrWhiteSpace(id);

    private static bool ValidateCurrentPrice(this decimal? currentPrice) => currentPrice is >= 0;

    private static bool ValidateLastUpdated(this DateTimeOffset? lastUpdated) => lastUpdated is not null;
}
