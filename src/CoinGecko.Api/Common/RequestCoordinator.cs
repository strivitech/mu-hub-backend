using System.Diagnostics;

using Microsoft.Extensions.Logging;

namespace CoinGecko.Api.Common;

/// <summary>
/// Request coordinator.
/// </summary>
public class RequestCoordinator : IRequestCoordinator
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RequestCoordinator> _logger;

    public RequestCoordinator(HttpClient httpClient, ILogger<RequestCoordinator> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<HttpResponseMessage> GetAsync(Uri resourceUri)
    {
        try
        {
            return await _httpClient.GetAsync(resourceUri).ConfigureAwait(false);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Http request exception");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while sending request");
            throw;
        }
    }

    public async Task<HttpResponseMessage> GetAsync(string resourceUri)
    {
        try
        {
            return await _httpClient.GetAsync(resourceUri).ConfigureAwait(false);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Http request exception");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while sending request");
            throw;
        }
    }
}
