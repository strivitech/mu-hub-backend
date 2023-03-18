namespace MuHub.Market.Proxy.Features.Coins;

/// <summary>
/// Represents the order of the coins.
/// </summary>
public sealed class Order
{
    public static readonly Order MarketCapDesc = new(CoinGecko.Api.Features.Coins.Order.MarketCapDesc);
    public static readonly Order MarketCapAsc = new(CoinGecko.Api.Features.Coins.Order.MarketCapAsc);

    private Order(CoinGecko.Api.Features.Coins.Order value)
    {
        Value = value;
    }

    internal CoinGecko.Api.Features.Coins.Order Value { get; }
}
