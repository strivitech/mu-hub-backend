using EFCore.BulkExtensions;

using MuHub.Application.Contracts.Persistence;
using MuHub.Domain.Entities;

namespace MuHub.Infrastructure.Persistence;

public class MarketCoinsWithTimeStampStorage : IMarketCoinsStorage
{
    private readonly IApplicationDbContext _applicationDbContext;

    public MarketCoinsWithTimeStampStorage(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task ReplaceAllMarketCoinsAsync(IList<MarketCoin> coins)
    {
        await using var transaction = await _applicationDbContext.Instance.Database.BeginTransactionAsync();
        try
        {
            await _applicationDbContext.Instance.TruncateAsync<MarketCoin>();
            await _applicationDbContext.Instance.BulkInsertAsync(coins);
            _applicationDbContext.MarketCoinsUpdateTimestamps.Add(new MarketCoinsUpdateTimestamp
            {
                LastUpdated = DateTime.UtcNow,
            });
            await _applicationDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            // Log error
            throw;
        }
    }
}
