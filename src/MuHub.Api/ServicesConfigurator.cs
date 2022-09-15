using Microsoft.AspNetCore.Mvc.Infrastructure;

using MuHub.Api.Common.Extensions.Startup;
using MuHub.Api.Common.Factories;

namespace MuHub.Api;

/// <summary>
/// Configures services for API layer.
/// </summary>
public static class ServicesConfigurator
{
    /// <summary>
    /// Adds services for API layer.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddControllers();

        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
        services.AddApiControllersVersioning();
        services.AddSwaggerServices();

        return services;
    }
}
