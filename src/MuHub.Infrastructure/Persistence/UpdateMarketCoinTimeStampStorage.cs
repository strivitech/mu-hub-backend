using Microsoft.EntityFrameworkCore;

using MuHub.Application.Contracts.Persistence;
using MuHub.Domain.Entities;

namespace MuHub.Infrastructure.Persistence;

public class UpdateMarketCoinTimeStampStorage : IUpdateMarketCoinTimeStampStorage
{
    private readonly IApplicationDbContext _applicationDbContext;

    public UpdateMarketCoinTimeStampStorage(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<MarketCoinsUpdateTimestamp?> GetLastUpdateTimeAsync()
    {
        return await _applicationDbContext.MarketCoinsUpdateTimestamps
            .OrderBy(x => x.LastUpdated)
            .LastOrDefaultAsync();
    }
}
