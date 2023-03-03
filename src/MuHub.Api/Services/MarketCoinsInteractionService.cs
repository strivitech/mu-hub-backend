using System.Diagnostics;
using System.Text.Json;

using EFCore.BulkExtensions;

using FluentResults;

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using MuHub.Api.Common.Constants;
using MuHub.Api.Hubs;
using MuHub.Api.Hubs.Clients;
using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Mapping;
using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Api.Services;

/// <summary>
/// 
/// </summary>
public class MarketCoinsInteractionService : IMarketCoinsInteractionService
{
    private readonly ICoinsDataService _coinsDataService;
    private readonly IMarketCoinsStorage _marketCoinsStorage;
    private readonly IUpdateMarketCoinTimeStampStorage _updateMarketCoinTimeStampStorage;
    private readonly IHubContext<CoinsHub, ICoinsClient> _hub;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coinsDataService"></param>
    /// <param name="marketCoinsStorage"></param>
    /// <param name="updateMarketCoinTimeStampStorage"></param>
    /// <param name="hub"></param>
    public MarketCoinsInteractionService(ICoinsDataService coinsDataService, IMarketCoinsStorage marketCoinsStorage,
        IUpdateMarketCoinTimeStampStorage updateMarketCoinTimeStampStorage, IHubContext<CoinsHub, ICoinsClient> hub)
    {
        _coinsDataService = coinsDataService;
        _marketCoinsStorage = marketCoinsStorage;
        _updateMarketCoinTimeStampStorage = updateMarketCoinTimeStampStorage;
        _hub = hub;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    // TODO: Add logging
    public async Task UpdateCoinInformation(IList<string> ids)
    {
        if (!ids.Any())
        {
            return;
        }

        CheckIfIdsCountIsDesired(ids);

        var lastUpdate = await _updateMarketCoinTimeStampStorage.GetLastUpdateTimeAsync();

        if (lastUpdate?.LastUpdated >
            DateTime.UtcNow.AddSeconds(-MarketCoinsInteractionConstants.ValidDataPeriodSeconds))
        {
            return;
        }

        var marketCoinDtos = await RetrieveMarketCoinsByIds(ids);
        
        await _marketCoinsStorage.ReplaceAllMarketCoinsAsync(marketCoinDtos.ToMarketCoins());

        // TODO: Update code
        // await _hub.Clients.All.UpdateCoinsInformation(marketCoins);
    }

    private async Task<List<MarketCoinDto>> RetrieveMarketCoinsByIds(IEnumerable<string> ids)
    {
        IEnumerable<string[]> chunks = ids.Chunk(MarketCoinsInteractionConstants.PerPage);
        var marketCoins = new List<MarketCoinDto>();
        foreach (var chunk in chunks)
        {
            var request = new GetMarketCoinRequest
            {
                Ids = chunk,
                PerPage = MarketCoinsInteractionConstants.PerPage,
                Order = Order.MarketCapDesc,
                Currency = Currency.Usd,
            };

            var result = await _coinsDataService.GetMarketCoinListAsync(request);
            if (result.IsFailed)
            {
                // Log error
                throw new InvalidOperationException(
                    $"Failed to get market coins with request {JsonSerializer.Serialize(request)}.");
            }

            marketCoins.AddRange(result.Value);
        }

        return marketCoins;
    }

    private static void CheckIfIdsCountIsDesired(IList<string> ids)
    {
        if (ids.Count > MarketCoinsInteractionConstants.DesiredIdsCountMaxValue)
        {
            // Log warning
        }
    }
}
