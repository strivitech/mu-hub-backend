using MuHub.Domain.Entities;

namespace MuHub.Application.Contracts.Persistence;

/// <summary>
/// Update market coin time stamp storage interface.
/// </summary>
public interface IUpdateMarketCoinTimeStampStorage
{
    /// <summary>
    /// Asynchronously updates last update time.
    /// </summary>
    /// <returns>Marks the task as complete.</returns>
    Task<MarketCoinsUpdateTimestamp?> GetLastUpdateTimeAsync();
}
