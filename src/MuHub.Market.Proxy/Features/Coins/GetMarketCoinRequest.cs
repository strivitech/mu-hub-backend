namespace MuHub.Market.Proxy.Features.Coins;

public class GetMarketCoinRequest
{
    public IEnumerable<string>? Ids { get; set; }

    public Order Order { get; set; } = Order.MarketCapDesc;
    
    public Currency Currency { get; set; } = Currency.Usd;
    
    /// <summary>
    /// The number of items to return.
    /// </summary>
    public int PerPage { get; set; } = 100;

    /// <summary>
    /// The page number.
    /// </summary>
    public int Page { get; set; } = 1;
}
