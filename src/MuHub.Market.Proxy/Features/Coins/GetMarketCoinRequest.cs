namespace MuHub.Market.Proxy.Features.Coins;

/// <summary>
/// Get market coin request.
/// </summary>
public class GetMarketCoinRequest
{
    /// <summary>
    /// Include only coins with the given ids.
    /// </summary>
    public IEnumerable<string>? Ids { get; set; }

    /// <summary>
    /// Order results by market cap.
    /// </summary>
    public Order Order { get; set; } = Order.MarketCapDesc;
    
    /// <summary>
    /// Currency to use when fetching market data.
    /// </summary>
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
