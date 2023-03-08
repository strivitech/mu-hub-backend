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
    private readonly IMarketCoinsInteractionService _marketCoinsInteractionService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="marketCoinsInteractionService"></param>
    public UpdateCoinsHangfireJob(IMarketCoinsInteractionService marketCoinsInteractionService)
    {
        _marketCoinsInteractionService = marketCoinsInteractionService;
    }

    /// <summary>
    /// 
    /// </summary>
    public async Task UpdateCoinInformation() => await _marketCoinsInteractionService.UpdateCoinInformation();
}
