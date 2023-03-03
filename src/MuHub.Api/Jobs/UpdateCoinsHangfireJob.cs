using System.Collections.Immutable;

using Microsoft.AspNetCore.SignalR;

using MuHub.Api.Hubs;
using MuHub.Api.Services;
using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Exceptions;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Api.Jobs;

/// <summary>
/// 
/// </summary>
public class UpdateCoinsHangfireJob
{
    private readonly ICoinsDataService _coinsDataService;
    private readonly ICoinsStorage _coinsStorage;
    private readonly IMarketCoinsInteractionService _marketCoinsInteractionService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="marketCoinsInteractionService"></param>
    /// <param name="coinsStorage"></param>
    public UpdateCoinsHangfireJob(IMarketCoinsInteractionService marketCoinsInteractionService,
        ICoinsDataService coinsDataService, ICoinsStorage coinsStorage)
    {
        _marketCoinsInteractionService = marketCoinsInteractionService;
        _coinsDataService = coinsDataService;
        _coinsStorage = coinsStorage;
    }

    /// <summary>
    /// 
    /// </summary>
    public async Task UpdateCoinInformation()
    {
        var coins = await _coinsStorage.GetAllAsync();

        if (!coins.Any())
        {
            throw new UnexpectedException("No coins found to update information");
        }

        await _marketCoinsInteractionService.UpdateCoinInformation(coins.Select(x => x.SymbolId).ToImmutableArray());
    }
}
