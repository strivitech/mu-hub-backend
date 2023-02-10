using Microsoft.Extensions.Logging;

namespace CoinGecko.Api.Common;

public abstract class DataProvider
{
    protected DataProvider(IRequestCoordinator requestCoordinator)
    {
        RequestCoordinator = requestCoordinator ?? throw new ArgumentNullException(nameof(requestCoordinator));
    }
    
    protected IRequestCoordinator RequestCoordinator { get; }
}
