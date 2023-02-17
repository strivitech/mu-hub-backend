using CoinGecko.Api.Features.Coins;

using FluentResults;

using MuHub.Market.Proxy.Mapping;

namespace MuHub.Market.Proxy.Features.Coins;

/// <summary>
/// Coins data service.
/// </summary>
public class CoinsDataService : ICoinsDataService
{
    private readonly ICoinsDataProvider _coinsDataProvider;

    public CoinsDataService(ICoinsDataProvider coinsDataProvider)
    {
        _coinsDataProvider = coinsDataProvider;
    }

    public async Task<Result<List<Coin>?>> GetCoinListAsync()
    {
        var result = await _coinsDataProvider.GetCoinListAsync();

        return result.IsFailed
            ? Result.Fail<List<Coin>?>(result.Errors)
            : Result.Ok(result.Value?.ToCoins());
    }
}
