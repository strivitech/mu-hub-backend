using System.Collections.Immutable;
using System.Text.Json;

using Microsoft.AspNetCore.SignalR;

using MuHub.Api.Common.Constants;
using MuHub.Api.Hubs;
using MuHub.Api.Hubs.Clients;
using MuHub.Api.Mapping;
using MuHub.Api.Responses;
using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Exceptions;
using MuHub.Application.Mapping;
using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Api.Services;

/// <summary>
/// Market coins interaction service.
/// </summary>
public class MarketCoinsInteractionService : IMarketCoinsInteractionService
{
    private readonly ICoinsDataService _coinsDataService;
    private readonly IMarketCoinsStorage _marketCoinsStorage;
    private readonly IUpdateMarketCoinTimeStampStorage _updateMarketCoinTimeStampStorage;
    private readonly ICoinsStorage _coinsStorage;
    private readonly IHubContext<CoinsHub, ICoinsClient> _hub;

    /// <summary>
    /// Initializes a new instance of the <see cref="MarketCoinsInteractionService"/> class.
    /// </summary>
    /// <param name="coinsDataService">Coins data service.</param>
    /// <param name="marketCoinsStorage">Market coins storage.</param>
    /// <param name="updateMarketCoinTimeStampStorage">Update market coin time stamp storage.</param>
    /// <param name="coinsStorage">Coins storage.</param>
    /// <param name="hub">Hub.</param>
    public MarketCoinsInteractionService(
        ICoinsDataService coinsDataService, 
        IMarketCoinsStorage marketCoinsStorage,
        IUpdateMarketCoinTimeStampStorage updateMarketCoinTimeStampStorage, 
        ICoinsStorage coinsStorage,
        IHubContext<CoinsHub, ICoinsClient> hub)
    {
        _coinsDataService = coinsDataService;
        _marketCoinsStorage = marketCoinsStorage;
        _updateMarketCoinTimeStampStorage = updateMarketCoinTimeStampStorage;
        _coinsStorage = coinsStorage;
        _hub = hub;
    }

    /// <summary>
    /// Updates coin information.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    // TODO: Add logging
    // TODO: Add Redis cache for first connected users
    public async Task UpdateCoinInformationAsync()
    {
        try
        {
            var coinsDictionary = await _coinsStorage.GetAllDictionaryByExternalSymbolIdAsync();
            ThrowIfNoCoinsFound(coinsDictionary);
        
            var ids = coinsDictionary.Keys.ToImmutableList();

            CheckIfIdsCountIsDesired(ids);

            var lastUpdate = await _updateMarketCoinTimeStampStorage.GetLastUpdateTimeAsync();

            if (lastUpdate?.LastUpdated >
                DateTimeOffset.UtcNow.AddSeconds(-MarketCoinsInteractionConstants.ValidDataPeriodSeconds))
            {
                return;
            }

            var marketCoinDtos = await RetrieveMarketCoinsByIds(ids);
            var marketCoins = marketCoinDtos.ToMarketCoins(coinsDictionary);

            await _marketCoinsStorage.ReplaceAllMarketCoinsAsync(marketCoins);
        
            await _hub.Clients.All.UpdateCoinsInformation(marketCoins.ToMarketCoinResponseEnumerable());
        }
        catch (Exception e)
        {
            // Log error
            throw;
        }
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

    private static void ThrowIfNoCoinsFound(IDictionary<string, Coin> coins)
    {
        if (!coins.Any())
        {
            throw new UnexpectedException("No coins found to update information");
        }
    }
}
