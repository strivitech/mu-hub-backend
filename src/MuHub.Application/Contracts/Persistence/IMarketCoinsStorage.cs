using MuHub.Domain.Entities;

namespace MuHub.Application.Contracts.Persistence;

public interface IMarketCoinsStorage
{
    Task ReplaceAllMarketCoinsAsync(IList<MarketCoin> coins);
}
