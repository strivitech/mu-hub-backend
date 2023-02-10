using CoinGecko.Api.Endpoints;
using CoinGecko.Api.Features.Coins;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Polly;
using Polly.Contrib.WaitAndRetry;

namespace CoinGecko.Api.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoinGeckoApi(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddScoped<ICoinsDataProvider, CoinsDataProvider>();
        services.TryAddScoped<IRequestCoordinator, RequestCoordinator>();
        services.AddHttpClientForRequestCoordinator();
        return services;
    }

    public static IServiceCollection AddHttpClientForRequestCoordinator(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddHttpClient<IRequestCoordinator, RequestCoordinator>(client =>
        {
            client.BaseAddress = GeneralEndpointsInfo.ApiUri;
        }).AddTransientHttpErrorPolicy(
        policyBuilder => policyBuilder.WaitAndRetryAsync(
                Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(RequestPolly.MedianFirstRetryDelaySeconds),
                    RequestPolly.DefaultRetryCount)));
            return services;
    }
}
