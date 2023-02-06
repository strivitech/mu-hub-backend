namespace CoinGecko.Api.Common;

public abstract class DataProvider
{
    protected DataProvider(IRequestCoordinator requestCoordinator)
    {
        RequestCoordinator = requestCoordinator;
    }
    
    protected IRequestCoordinator RequestCoordinator { get; }
}
