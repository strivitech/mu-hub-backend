namespace CoinGecko.Api.Features.Coins;

/// <summary>
/// Currency to use for market data conversion.
/// </summary>
public sealed class VsCurrency
{
    public static readonly VsCurrency Usd = new ("usd");
    public static readonly VsCurrency Eur = new ("eur");
    
    private VsCurrency(string value) => Value = value;
    public string Value { get; }
}
