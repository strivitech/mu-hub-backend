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
    bool IsValid)
{
    public string Id { get; } = Id;
    
    public string Name { get; } = Name;
    
    public string Symbol { get; } = Symbol;

    public string? ImageUrl { get; } = ImageUrl;

    public decimal? CurrentPrice { get; } = CurrentPrice;
    
    public decimal? High24H { get; } = High24H;

    public decimal? Low24H { get; } = Low24H;

    public DateTimeOffset? LastUpdated { get; } = LastUpdated;

    public bool IsValid { get; } = IsValid;
}
