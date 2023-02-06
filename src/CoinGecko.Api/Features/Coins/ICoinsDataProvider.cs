using FluentResults;

namespace CoinGecko.Api.Features.Coins;

public interface ICoinsDataProvider
{
    /// <summary>
    /// List all supported coins id, name and symbol
    /// </summary>
    /// <param name="includePlatform"></param>
    /// <returns></returns>
    Task<Result<List<Coin>?>> GetCoinListAsync(bool includePlatform = false);
}
