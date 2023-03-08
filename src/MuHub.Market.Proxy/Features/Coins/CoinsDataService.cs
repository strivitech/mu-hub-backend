using CoinGecko.Api.Features.Coins;

using FluentResults;

using MuHub.Market.Proxy.Features.Coins.Mapping;

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

    public async Task<Result<List<MarketCoinDto>>> GetMarketCoinListAsync(GetMarketCoinRequest request)
    {
        var requestToApi = request.MapFromGetMarketCoinRequest();
        
        var result = await _coinsDataProvider.GetMarketCoinListAsync(requestToApi);

        return result.IsFailed
            ? Result.Fail<List<MarketCoinDto>>(result.Errors)
            : Result.Ok(result.Value.ToMarketCoinDtoList());
    }
}
