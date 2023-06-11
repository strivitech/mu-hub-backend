using MuHub.Application.Models.Data.WatchList;

namespace MuHub.Application.Services.Interfaces;

public interface IWatchListService
{
    Task<bool> AddToWatchListAsync(int coinId, string userId);
    
    Task<UserWatchListCollection?> GetWatchListsByUserAsync(string userId);
}