namespace CoinGecko.Api.Features.Coins;

public class GetMarketCoinRequest
{
    public VsCurrency VsCurrency { get; set; } = VsCurrency.Usd;
    public string? Ids { get; set; }
    public string? Category { get; set; }
    public Order Order { get; set; } = Order.MarketCapDesc;
    public int PerPage { get; set; } = 100;
    public int Page { get; set; } = 1;
    public bool Sparkline { get; set; } = false;
    public string? PriceChangePercentage { get; set; }
}
