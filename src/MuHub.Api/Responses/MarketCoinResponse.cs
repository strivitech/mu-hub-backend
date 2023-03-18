namespace MuHub.Api.Responses;

/// <summary>
/// Market coin response.
/// </summary>
/// <param name="Id">Id.</param>
/// <param name="Name">Name.</param>
/// <param name="Symbol">Symbol.</param>
/// <param name="CurrentPrice">Current price.</param>
/// <param name="LastUpdated">Last updated.</param>
/// <param name="ImageUrl">Image URL.</param>
/// <param name="High24H">High 24H.</param>
/// <param name="Low24H">Low 24H.</param>
/// <param name="IsValid">Is valid.</param>
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
