namespace CoinGecko.Api.Common;

public interface IRequestCoordinator
{
    Task<HttpResponseMessage> GetAsync(Uri resourceUri);

    Task<HttpResponseMessage> GetAsync(string resourceUri);
}
