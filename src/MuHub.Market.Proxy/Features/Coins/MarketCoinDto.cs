namespace MuHub.Market.Proxy.Features.Coins;

public record MarketCoinDto(
    string Id,
    decimal? CurrentPrice,
    int? MarketCapRank,
    DateTimeOffset? LastUpdated,
    string? ImageUrl,
    decimal? High24H,
    decimal? Low24H,
    bool IsValid);
