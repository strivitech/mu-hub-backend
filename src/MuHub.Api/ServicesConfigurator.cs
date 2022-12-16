using System.Reflection;
using System.Text;

using Amazon.CognitoIdentityProvider;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;

using MuHub.Api.Common.Extensions;
using MuHub.Api.Common.Extensions.Startup;
using MuHub.Api.Common.Factories;
using MuHub.Api.Common.Filters;
using MuHub.Api.Services;
using MuHub.Application.Contracts.Infrastructure;
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
    /// <param name="configuration">Configuration.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddCognitoAuthentication(configuration);
        
        services.AddControllers(options =>
        {
            options.Filters.Add<ApiModelValidationFilter>();
        })
            .RegisterValidatorsInAssemblyList(new List<Assembly>
            {
                typeof(ServicesConfigurator).Assembly,
            });

        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
        services.AddScoped<IAmazonCognitoIdentityProvider, AmazonCognitoIdentityProviderClient>();
        services.AddScoped<IIdentityProvider, IdentityProvider>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IUserSessionData, CurrentUserSessionData>();
        services.AddApiControllersVersioning();
        services.AddSwaggerServices();

        return services;
    }
}
