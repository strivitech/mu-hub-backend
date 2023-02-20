using System.Reflection;

using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using MuHub.Api.Common.Extensions;
using MuHub.Api.Common.Extensions.Startup;
using MuHub.Api.Common.Factories;
using MuHub.Api.Common.Filters;
using MuHub.Api.Services;
using MuHub.Application.Contracts.Infrastructure;
using MuHub.Market.Proxy;
using MuHub.Permissions.Policy;


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
    /// <param name="environment"></param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        bool isDevelopment = environment.IsDevelopment();
        
        services.AddHttpContextAccessor();

        Assembly[] assembliesToScan = 
        {
            typeof(ServicesConfigurator).Assembly,
            typeof(Application.ServicesConfigurator).Assembly,
            typeof(Infrastructure.ServicesConfigurator).Assembly,
        };
        
        services.AddValidatorsFromAssemblies(assembliesToScan);
        
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

                options.RequireHttpsMetadata = !isDevelopment;
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
        
        services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IUserSessionData, CurrentUserSessionData>();

        services.AddMarketProxy();
        
        services.AddApiControllersVersioning();
        services.AddSwaggerServices();

        return services;
    }
}
