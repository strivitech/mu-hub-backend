namespace MuHub.Market.Proxy.Features.Coins;

public record MarketCoinDto(
    string Id,
    decimal? CurrentPrice,
    int? MarketCapRank,
    DateTimeOffset? LastUpdated,
    string? ImageUrl,
    decimal? High24H,
    decimal? Low24H,
    bool IsValid)
{
    public string Id { get; } = Id;

    public string? ImageUrl { get; } = ImageUrl;

    public decimal? CurrentPrice { get; } = CurrentPrice;
    
    public int? MarketCapRank { get; } = MarketCapRank;

    public decimal? High24H { get; } = High24H;

    public decimal? Low24H { get; } = Low24H;

    public DateTimeOffset? LastUpdated { get; } = LastUpdated;

    public bool IsValid { get; } = IsValid;
}
