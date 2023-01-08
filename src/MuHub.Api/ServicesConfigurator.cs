using System.Reflection;
using System.Text;

using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;

using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MuHub.Api.Common.Configurations.Identity;
using MuHub.Api.Common.Configurations.User;
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

        var cognitoConfig = configuration.GetSection(AwsCognitoAuthenticationConfiguration.SectionName)
            .Get<AwsCognitoAuthenticationConfiguration>();
        services.AddOptions<AwsCognitoUserPoolOptions>().Configure(x =>
            x.UserPoolId = cognitoConfig.UserPoolId);

        services.AddCognitoAuthentication(cognitoConfig);

        Assembly[] assembliesToScan = 
        {
            typeof(ServicesConfigurator).Assembly,
            typeof(Application.ServicesConfigurator).Assembly,
            typeof(Infrastructure.ServicesConfigurator).Assembly
        };
        services.AddAutoMapper(assembliesToScan);
        services.AddValidatorsFromAssemblies(assembliesToScan);

        services.AddControllers(options =>
            {
                options.Filters.Add<ApiModelValidationFilter>();
            })
            .RegisterValidatorsInAssemblyList(new List<Assembly> { typeof(ServicesConfigurator).Assembly, });

        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

        var awsUser = configuration.GetSection(AwsUserConfiguration.SectionName).Get<AwsUserConfiguration>();
        var awsBasicCredentials = new BasicAWSCredentials(awsUser.AccessKey, awsUser.SecretKey);
        services.AddScoped<IAmazonCognitoIdentityProvider>(_ =>
            new AmazonCognitoIdentityProviderClient(awsBasicCredentials,
                RegionEndpoint.GetBySystemName(cognitoConfig.Region)));
        services.AddScoped<IIdentityProvider, IdentityProvider>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IUserSessionData, CurrentUserSessionData>();
        services.AddApiControllersVersioning();
        services.AddSwaggerServices();

        return services;
    }
}
