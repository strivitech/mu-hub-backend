using System.Globalization;
using System.Text.Json;

using CoinGecko.Api.Common;
using CoinGecko.Api.Endpoints;

using FluentResults;

namespace CoinGecko.Api.Features.Coins;

public class CoinsDataProvider : DataProvider, ICoinsDataProvider
{
    public CoinsDataProvider(IRequestCoordinator requestCoordinator) : base(requestCoordinator)
    {
    }

    public async Task<Result<List<Coin>?>> GetCoinListAsync(bool includePlatform = false)
    {
        var requestUri = QueryBuilder.Create(GeneralEndpointsInfo.ApiUriStringValue)
            .AddPath(CoinEndpoints.CoinList)
            .AddQueryParameters(new Dictionary<string, string>
            {
                ["include_platform"] = includePlatform.ToString(CultureInfo.InvariantCulture),
            })
            .Build();

        try
        {
            var response = await RequestCoordinator.GetAsync(requestUri).ConfigureAwait(false);
            return !response.IsSuccessStatusCode
                ? Result.Fail<List<Coin>?>(response.ReasonPhrase)
                : Result.Ok(await response.ReadContentAsAsync<List<Coin>>());
        }
        catch (Exception ex)
        {
            // TODO: Log exception
            return Result.Fail<List<Coin>?>(new ExceptionalError(ex));
        }
    }
}
