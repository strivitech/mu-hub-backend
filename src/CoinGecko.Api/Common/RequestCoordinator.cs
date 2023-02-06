using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;

namespace CoinGecko.Api.Common;

public class RequestCoordinator : IRequestCoordinator
{
    private readonly HttpClient _httpClient;

    private readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy =
        Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrTransientHttpError()
            .AdvancedCircuitBreakerAsync(0.5, TimeSpan.FromSeconds(10),
                10, TimeSpan.FromSeconds(15));

    public RequestCoordinator(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // TODO: review catch blocks
    public async Task<HttpResponseMessage> GetAsync(Uri resourceUri)
    {
        try
        {
            return await _circuitBreakerPolicy.ExecuteAsync(() => _httpClient.GetAsync(resourceUri))
                .ConfigureAwait(false);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // TODO: review catch blocks
    public async Task<HttpResponseMessage> GetAsync(string resourceUri)
    {
        try
        {
            return await _circuitBreakerPolicy.ExecuteAsync(() => _httpClient.GetAsync(resourceUri))
                .ConfigureAwait(false);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
