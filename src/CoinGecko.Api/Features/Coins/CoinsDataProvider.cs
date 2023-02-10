using System.Diagnostics;
using System.Globalization;
using System.Text.Json;

using CoinGecko.Api.Common;
using CoinGecko.Api.Endpoints;

using FluentResults;

using Microsoft.Extensions.Logging;

namespace CoinGecko.Api.Features.Coins;

public class CoinsDataProvider : DataProvider, ICoinsDataProvider
{
    private readonly ILogger<CoinsDataProvider> _logger;

    public CoinsDataProvider(IRequestCoordinator requestCoordinator, ILogger<CoinsDataProvider> logger) : base(requestCoordinator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<List<Coin>?>> GetCoinListAsync(bool includePlatform = false)
    {
        _logger.LogDebug("Getting coin list started");
        
        var requestUri = QueryBuilder.Create(GeneralEndpointsInfo.ApiUriStringValue)
            .AddPath(CoinEndpoints.CoinList)
            .AddQueryParameters(new Dictionary<string, string>
            {
                ["include_platform"] = includePlatform.ToString(CultureInfo.InvariantCulture),
            })
            .Build();

        try
        {
            _logger.LogInformation("Sending request to {RequestUri}", requestUri);
            var response = await RequestCoordinator.GetAsync(requestUri).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Result.Fail<List<Coin>?>(response.ReasonPhrase)
                : Result.Ok(await response.ReadContentAsAsync<List<Coin>>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while getting coin list");
            return Result.Fail<List<Coin>?>(new ExceptionalError(ex));
        }
    }
}
