﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MuHub.Application.Contracts.Infrastructure;
using MuHub.Application.Contracts.Persistence;
using MuHub.Infrastructure.Persistence;
using MuHub.Infrastructure.Services;

namespace MuHub.Infrastructure;

public static class ServicesConfigurator
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IApplicationDbContextInstanceResolver>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IFakeEmailSender, FakeEmailSender>();
        services.AddScoped<IFakeStorage, FakeStorage>();
        services.AddScoped<IMarketCoinsStorage, MarketCoinsWithTimeStampStorage>();
        services.AddScoped<IUpdateMarketCoinTimeStampStorage, UpdateMarketCoinTimeStampStorage>();
        services.AddScoped<ICoinsStorage, CoinsStorage>();

        return services;
    }
}
