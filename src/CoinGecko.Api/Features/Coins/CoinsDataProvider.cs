using System.Diagnostics;
using System.Globalization;
using System.Text.Json;

using CoinGecko.Api.Common;
using CoinGecko.Api.Endpoints;

using FluentResults;

using Microsoft.Extensions.Logging;

namespace CoinGecko.Api.Features.Coins;

/// <summary>
/// Coins data provider.
/// </summary>
public class CoinsDataProvider : DataProvider, ICoinsDataProvider
{
    private readonly ILogger<CoinsDataProvider> _logger;

    public CoinsDataProvider(IRequestCoordinator requestCoordinator, ILogger<CoinsDataProvider> logger) : base(
        requestCoordinator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<List<Coin>>> GetCoinListAsync(bool includePlatform = false)
    {
        _logger.LogDebug("Getting coin list started");

        var requestUri = QueryBuilder.Create(GeneralEndpointsInfo.ApiUriStringValue)
            .AddPath(EndpointsMap.Coin.CoinList)
            .AddQueryParameters(new Dictionary<string, string>
            {
                [QueryParametersConstants.Coin.List.IncludePlatform] =
                    includePlatform.ToString(CultureInfo.InvariantCulture),
            })
            .Build();

        return await GetListOfTAsync<Coin>(requestUri).ConfigureAwait(false);
    }

    public async Task<Result<List<MarketCoin>>> GetMarketCoinListAsync(GetMarketCoinRequest request)
    {
        _logger.LogDebug("Getting coin list started");

        var requestUri = QueryBuilder.Create(GeneralEndpointsInfo.ApiUriStringValue)
            .AddPath(EndpointsMap.Coin.CoinMarkets)
            .AddQueryParameters(CreateGetMarketCoinListQueryDictionary(request))
            .Build();

        return await GetListOfTAsync<MarketCoin>(requestUri).ConfigureAwait(false);
    }

    private async Task<Result<List<T>>> GetListOfTAsync<T>(Uri requestUri)
    {
        try
        {
            _logger.LogInformation("Sending request to {RequestUri}", requestUri);
            var response = await RequestCoordinator.GetAsync(requestUri).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Result.Fail<List<T>>(response.ReasonPhrase)
                : Result.Ok(await response.ReadContentAsAsync<List<T>>().ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while getting list");
            return Result.Fail<List<T>>(new ExceptionalError(ex));
        }
    }

    private static Dictionary<string, string> CreateGetMarketCoinListQueryDictionary(GetMarketCoinRequest request)
    {
        var queryDictionary = new Dictionary<string, string>
        {
            [QueryParametersConstants.Coin.Market.VsCurrency] = request.VsCurrency.Value,
            [QueryParametersConstants.Coin.Market.Order] = request.Order.Value,
            [QueryParametersConstants.Coin.Market.PerPage] = request.PerPage.ToString(),
            [QueryParametersConstants.Coin.Market.Page] = request.Page.ToString(),
            [QueryParametersConstants.Coin.Market.Sparkline] = request.Sparkline.ToString(),
        };

        if (request.Ids is not null)
        {
            queryDictionary[QueryParametersConstants.Coin.Market.Ids] = request.Ids;
        }

        if (request.Category is not null)
        {
            queryDictionary[QueryParametersConstants.Coin.Market.Category] = request.Category;
        }

        if (request.PriceChangePercentage is not null)
        {
            queryDictionary[QueryParametersConstants.Coin.Market.PriceChangePercentage] = request.PriceChangePercentage;
        }
        
        return queryDictionary;
    }
}
