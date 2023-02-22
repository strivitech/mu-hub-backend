using System.Text.Json.Serialization;

namespace MuHub.Market.Proxy.Features.Coins;

/// <summary>
/// Coin.
/// </summary>
public record CoinDto(string Id, string Symbol, string Name)
{
    /// <summary>
    /// Id.
    /// </summary>
    public string Id { get; } = Id;

    /// <summary>
    /// Symbol.
    /// </summary>
    public string Symbol { get; } = Symbol;

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; } = Name;
}
