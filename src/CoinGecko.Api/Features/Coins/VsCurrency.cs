namespace CoinGecko.Api.Features.Coins;

public sealed class VsCurrency
{
    public static readonly VsCurrency Usd = new ("usd");
    public static readonly VsCurrency Eur = new ("eur");
    
    private VsCurrency(string value) => Value = value;
    public string Value { get; }
}
