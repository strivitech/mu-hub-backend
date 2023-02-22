namespace MuHub.Market.Proxy.Features.Coins;

public record MarketCoinDto(
    string Id,
    string Symbol,
    string Name,
    decimal CurrentPrice,
    DateTimeOffset LastUpdated,
    string? ImageUrl = null,
    decimal? High24H = null,
    decimal? Low24H = null)
{
    public string Id { get; } = Id;

    public string Symbol { get; } = Symbol;

    public string Name { get; } = Name;

    public string? ImageUrl { get; } = ImageUrl;

    public decimal CurrentPrice { get; } = CurrentPrice;

    public decimal? High24H { get; } = High24H;

    public decimal? Low24H { get; } = Low24H;

    public DateTimeOffset LastUpdated { get; } = LastUpdated;
}
