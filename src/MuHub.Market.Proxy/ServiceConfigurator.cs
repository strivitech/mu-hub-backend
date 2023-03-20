using CoinGecko.Api.Common;

using Microsoft.Extensions.DependencyInjection;

using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Market.Proxy;

public static class ServiceConfigurator
{
    public static IServiceCollection AddMarketProxy(this IServiceCollection services)
    {
        services.AddScoped<ICoinsDataService, CoinsDataService>();
        services.AddCoinGeckoApi();
        return services;
    }
}
