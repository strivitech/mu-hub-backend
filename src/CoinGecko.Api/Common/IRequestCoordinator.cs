namespace CoinGecko.Api.Common;

/// <summary>
/// Request coordinator.
/// </summary>
public interface IRequestCoordinator
{
    /// <summary>
    /// Asynchronously gets the resource by the given <see cref="Uri"/>.
    /// </summary>
    /// <param name="resourceUri">Resource uri.</param>
    /// <returns>The result of GET request represented in <see cref="HttpResponseMessage"/>.</returns>
    Task<HttpResponseMessage> GetAsync(Uri resourceUri);

    /// <summary>
    /// Asynchronously gets the resource by <see cref="string"/> uri.
    /// </summary>
    /// <param name="resourceUri">Resource uri.</param>
    /// <returns>The result of GET request represented in <see cref="HttpResponseMessage"/>.</returns>
    Task<HttpResponseMessage> GetAsync(string resourceUri);
}
