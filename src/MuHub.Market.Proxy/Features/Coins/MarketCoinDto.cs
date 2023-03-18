namespace MuHub.Market.Proxy.Features.Coins;

/// <summary>
/// Market coin DTO.
/// </summary>
/// <param name="Id">Id.</param>
/// <param name="CurrentPrice">Current price.</param>
/// <param name="MarketCapRank">Market cap rank.</param>
/// <param name="LastUpdated">Last updated.</param>
/// <param name="ImageUrl">Image URL.</param>
/// <param name="High24H">High 24H.</param>
/// <param name="Low24H">Low 24H.</param>
/// <param name="IsValid">Is valid.</param>
public record MarketCoinDto(
    string Id,
    decimal? CurrentPrice,
    int? MarketCapRank,
    DateTimeOffset? LastUpdated,
    string? ImageUrl,
    decimal? High24H,
    decimal? Low24H,
    bool IsValid);
