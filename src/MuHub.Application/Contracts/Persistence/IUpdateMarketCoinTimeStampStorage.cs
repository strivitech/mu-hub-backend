using MuHub.Domain.Entities;

namespace MuHub.Application.Contracts.Persistence;

public interface IUpdateMarketCoinTimeStampStorage
{
    Task<MarketCoinsUpdateTimestamp?> GetLastUpdateTimeAsync();
}
