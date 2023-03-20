using System.Collections.Immutable;
using MuHub.Api.Services;
using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Exceptions;

namespace MuHub.Api.Jobs;

/// <summary>
/// Update Coins Hangfire Job.
/// </summary>
public class UpdateCoinsHangfireJob
{
    private readonly IMarketCoinsInteractionService _marketCoinsInteractionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCoinsHangfireJob"/> class.
    /// </summary>
    /// <param name="marketCoinsInteractionService">Market Coins Interaction Service.</param>
    public UpdateCoinsHangfireJob(IMarketCoinsInteractionService marketCoinsInteractionService)
    {
        _marketCoinsInteractionService = marketCoinsInteractionService;
    }

    /// <summary>
    /// Updates Coin Information.
    /// </summary>
    public async Task UpdateCoinInformation() => await _marketCoinsInteractionService.UpdateCoinInformationAsync();
}
