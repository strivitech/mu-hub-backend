using System.Collections.Immutable;
using MuHub.Api.Services;
using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Exceptions;

namespace MuHub.Api.Jobs;

/// <summary>
/// 
/// </summary>
public class UpdateCoinsHangfireJob
{
    private readonly ICoinsStorage _coinsStorage;
    private readonly IMarketCoinsInteractionService _marketCoinsInteractionService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="marketCoinsInteractionService"></param>
    /// <param name="coinsStorage"></param>
    public UpdateCoinsHangfireJob(IMarketCoinsInteractionService marketCoinsInteractionService, ICoinsStorage coinsStorage)
    {
        _marketCoinsInteractionService = marketCoinsInteractionService;
        _coinsStorage = coinsStorage;
    }

    /// <summary>
    /// 
    /// </summary>
    public async Task UpdateCoinInformation()
    {
        var coinsIds = await _coinsStorage.GetAllIdsAsync();

        if (!coinsIds.Any())
        {
            throw new UnexpectedException("No coins found to update information");
        }

        await _marketCoinsInteractionService.UpdateCoinInformation(coinsIds);
    }
}
