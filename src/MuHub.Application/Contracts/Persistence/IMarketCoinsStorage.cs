using MuHub.Domain.Entities;

namespace MuHub.Application.Contracts.Persistence;

/// <summary>
/// Market coins storage interface.
/// </summary>
public interface IMarketCoinsStorage
{
    /// <summary>
    /// Asynchronously replaces all market coins.
    /// </summary>
    /// <param name="coins">Coins to replace.</param>
    /// <returns>An instance of <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task ReplaceAllMarketCoinsAsync(IList<MarketCoin> coins);
}
