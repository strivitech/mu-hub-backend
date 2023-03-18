namespace MuHub.Api.Responses;

/// <summary>
/// 
/// </summary>
public record MarketCoinResponse(
    string Id,
    string Name,
    string Symbol,
    decimal? CurrentPrice,
    DateTimeOffset? LastUpdated,
    string? ImageUrl,
    decimal? High24H,
    decimal? Low24H,
    bool IsValid);
