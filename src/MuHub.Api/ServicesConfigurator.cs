using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using MuHub.Api.Common.Extensions;
using MuHub.Api.Common.Extensions.Startup;
using MuHub.Api.Common.Factories;
using MuHub.Api.Common.Filters;
using MuHub.Application.Models.Requests.Info;

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

        services.AddControllers(options =>
        {
            options.Filters.Add<ApiModelValidationFilter>();
        })
            .RegisterValidatorsInAssemblyList(new List<Assembly>
            {
                typeof(Application.ServicesConfigurator).Assembly,
                typeof(Infrastructure.ServicesConfigurator).Assembly,
                typeof(ServicesConfigurator).Assembly,
            });

        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
        services.AddApiControllersVersioning();
        services.AddSwaggerServices();

        return services;
    }
}
