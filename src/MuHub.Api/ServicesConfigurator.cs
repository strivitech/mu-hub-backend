using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.AspNetCore.Mvc.Infrastructure;

using MuHub.Api.Common.Extensions;
using MuHub.Api.Common.Extensions.Startup;
using MuHub.Api.Common.Factories;
using MuHub.Api.Common.Filters;


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
    /// <param name="configuration"></param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddControllers(options =>
            {
                options.Filters.Add<ApiModelValidationFilter>();
            })
            .RegisterValidatorsInAssemblyList(new List<Assembly> { typeof(ServicesConfigurator).Assembly, });

        var authority = configuration["Identity:Authority"];
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = authority;
                options.Audience = "muhubapi";

                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
            });

        services.AddCors(config =>
            config.AddPolicy(
                "AllowAll",
                p => p.WithOrigins(configuration["AllowedCorsOrigins"].Split(';'))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()));

        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
        services.AddApiControllersVersioning();
        services.AddSwaggerServices();

        return services;
    }
}
