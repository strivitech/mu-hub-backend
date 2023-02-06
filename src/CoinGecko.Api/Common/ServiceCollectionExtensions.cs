using CoinGecko.Api.Endpoints;
using CoinGecko.Api.Features.Coins;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CoinGecko.Api.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoinGeckoApi(this IServiceCollection services)
    {
        services.AddScoped<ICoinsDataProvider, CoinsDataProvider>(); // TODO: add factory
        services.TryAddTransient<IRequestCoordinator, RequestCoordinator>();
        services.AddHttpClient<IRequestCoordinator, RequestCoordinator>(client =>
        {
            client.BaseAddress = GeneralEndpointsInfo.ApiUri;
        });
        return services;
    }
}
