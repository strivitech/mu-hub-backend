namespace CoinGecko.Api.Features.Coins;

/// <summary>
/// Get market coin request.
/// </summary>
public class GetMarketCoinRequest
{
    /// <summary>
    /// Currency.
    /// </summary>
    public VsCurrency VsCurrency { get; set; } = VsCurrency.Usd;
    
    /// <summary>
    /// Ids.
    /// </summary>
    public string? Ids { get; set; }
    
    /// <summary>
    /// Category.
    /// </summary>
    public string? Category { get; set; }
    
    /// <summary>
    /// Order.
    /// </summary>
    public Order Order { get; set; } = Order.MarketCapDesc;
    
    /// <summary>
    /// Per page.
    /// </summary>
    public int PerPage { get; set; } = 100;
    
    /// <summary>
    /// Page.
    /// </summary>
    public int Page { get; set; } = 1;
    
    /// <summary>
    /// Sparkline.
    /// </summary>
    public bool Sparkline { get; set; } = false;
    
    /// <summary>
    /// Price change percentage.
    /// </summary>
    public string? PriceChangePercentage { get; set; }
}
