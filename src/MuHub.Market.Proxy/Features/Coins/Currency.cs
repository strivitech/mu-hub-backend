namespace MuHub.Market.Proxy.Features.Coins;

/// <summary>
/// Represents a currency.
/// </summary>
public sealed class Currency
{
    public static readonly Currency Usd = new(CoinGecko.Api.Features.Coins.VsCurrency.Usd);
    public static readonly Currency Eur = new(CoinGecko.Api.Features.Coins.VsCurrency.Eur);

    private Currency(CoinGecko.Api.Features.Coins.VsCurrency value)
    {
        Value = value;
    }

    internal CoinGecko.Api.Features.Coins.VsCurrency Value { get; }
}
