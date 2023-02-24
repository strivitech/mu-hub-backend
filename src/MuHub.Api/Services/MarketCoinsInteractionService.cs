using System.Text.Json;

using FluentResults;

using Microsoft.AspNetCore.SignalR;

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
    private readonly IMarketCoinStorage _marketCoinsStorage;
    private readonly IHubContext<CoinsHub, ICoinsClient> _hub;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coinsDataService"></param>
    /// <param name="marketCoinsStorage"></param>
    /// <param name="hub"></param>
    public MarketCoinsInteractionService(ICoinsDataService coinsDataService, IMarketCoinStorage marketCoinsStorage,
        IHubContext<CoinsHub, ICoinsClient> hub)
    {
        _coinsDataService = coinsDataService;
        _marketCoinsStorage = marketCoinsStorage;
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

        if (ids.Count > MarketCoinsInteractionConstants.DesiredIdsCountMaxValue)
        {
            // Log warning
        }

        IEnumerable<string[]> chunks = ids.Chunk(MarketCoinsInteractionConstants.PerPage);
        int pageNumber = 1;
        var marketCoins = new List<MarketCoinDto>();
        foreach (var chunk in chunks)
        {
            var request = new GetMarketCoinRequest
            {
                Ids = chunk,
                PerPage = MarketCoinsInteractionConstants.PerPage,
                Page = pageNumber++,
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

            pageNumber++;
        }

        await _marketCoinsStorage.ReplaceAllMarketCoinsAsync(marketCoins.ToMarketCoins());
        // TODO: Update code
        await _hub.Clients.All.UpdateCoinsInformation(marketCoins);
    }
}
