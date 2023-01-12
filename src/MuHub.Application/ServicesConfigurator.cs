using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using MuHub.Application.Services.Implementations;
using MuHub.Application.Services.Interfaces;

namespace MuHub.Application;

public static class ServicesConfigurator
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IModelValidationService, ModelValidationService>();
        services.AddScoped<IInfoService, InfoService>();

        return services;
    }
}
