namespace CoinGecko.Api.Features.Coins;

/// <summary>
/// Order.
/// </summary>
public sealed class Order
{
    public static readonly Order MarketCapAsc = new ("market_cap_asc");
    public static readonly Order MarketCapDesc = new ("market_cap_desc");
    public static readonly Order GeckoDesc = new ("gecko_desc");
    public static readonly Order GeckoAsc = new ("gecko_asc");
    public static readonly Order VolumeAsc = new ("volume_asc");
    public static readonly Order VolumeDesc = new ("volume_desc");
    public static readonly Order IdAsc = new ("id_asc");
    public static readonly Order IdDesc = new ("id_desc");

    private Order(string value) => Value = value;
    public string Value { get; }
}
