using Microsoft.Extensions.Logging;

namespace CoinGecko.Api.Common;

/// <summary>
/// Data provider base class.
/// </summary>
public abstract class DataProvider
{
    protected DataProvider(IRequestCoordinator requestCoordinator)
    {
        RequestCoordinator = requestCoordinator ?? throw new ArgumentNullException(nameof(requestCoordinator));
    }
    
    /// <summary>
    /// Coordinates requests.
    /// </summary>
    protected IRequestCoordinator RequestCoordinator { get; }
}
