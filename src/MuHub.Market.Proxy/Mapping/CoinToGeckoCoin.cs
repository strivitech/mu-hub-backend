using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Market.Proxy.Mapping;

/// <summary>
/// Mapping from CoinGecko coin to MuHub coin.
/// </summary>
public static class CoinToGeckoCoin
{
    /// <summary>
    /// Maps from CoinGecko coin to MuHub coin.
    /// </summary>
    /// <param name="coin">Coin.</param>
    /// <returns><see cref="Coin"/>.</returns>
    public static Coin ToCoin(this CoinGecko.Api.Features.Coins.Coin coin)
    {
        return new Coin
        {
            Id = coin.Id,
            Name = coin.Name,
            Symbol = coin.Symbol,
        };
    }
    
    /// <summary>
    /// Maps from CoinGecko coins list to MuHub coins list.
    /// </summary>
    /// <param name="coins">Coins.</param>
    /// <returns><see cref="List{T}"/> of <see cref="Coin"/>.</returns>
    public static List<Coin> ToCoins(this IEnumerable<CoinGecko.Api.Features.Coins.Coin> coins)
    {
        return coins.Select(ToCoin).ToList();
    }
}
