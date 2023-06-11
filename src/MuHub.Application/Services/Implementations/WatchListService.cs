using Microsoft.EntityFrameworkCore;

using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Models.Data.WatchList;
using MuHub.Application.Services.Interfaces;
using MuHub.Domain.Entities;

namespace MuHub.Application.Services.Implementations;

public class WatchListService : IWatchListService
{
    private readonly IApplicationDbContext _dbContext;

    public WatchListService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddToWatchListAsync(int coinId, string userId)
    {
        if (!ParametersValid() || !await _dbContext.Coins.AnyAsync(x => x.Id == coinId))
        {
            return false;
        }

        try
        {
            var watchList = new WatchList { CoinId = coinId, UserId = userId };

            _dbContext.WatchList.Add(watchList);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

        bool ParametersValid()
        {
            return !string.IsNullOrEmpty(userId) && coinId > 0;
        }
    }

    public async Task<UserWatchListCollection?> GetWatchListsByUserAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return null;
        }

        var watchListCollection = await _dbContext.WatchList
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return new UserWatchListCollection { CoinIds = watchListCollection.Select(x => x.CoinId).ToList() };
    }
}
