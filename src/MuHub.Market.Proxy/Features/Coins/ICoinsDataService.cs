using FluentResults;

namespace MuHub.Market.Proxy.Features.Coins;

/// <summary>
/// Coins data service.
/// </summary>
public interface ICoinsDataService
{
    /// <summary>
    /// Asynchronously gets the coin list.
    /// </summary>
    /// <returns>A <see cref="Result"/> of <see cref="List{T}"/> of <see cref="Coin"/>.</returns>
    Task<Result<List<Coin>>> GetCoinListAsync();
}
