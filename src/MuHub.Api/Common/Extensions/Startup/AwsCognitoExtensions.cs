using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using MuHub.Api.Common.Configurations.Identity;

namespace MuHub.Api.Common.Extensions.Startup;

/// <summary>
/// 
/// </summary>
public static class AwsCognitoExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="cognitoConfig"></param>
    /// <returns></returns>
    public static IServiceCollection AddCognitoAuthentication(this IServiceCollection services,
        AwsCognitoAuthenticationConfiguration cognitoConfig)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(cognitoConfig);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = cognitoConfig.ValidIssuer,
                    ValidateIssuerSigningKey = cognitoConfig.ValidateIssuerSigningKey,
                    ValidateIssuer = cognitoConfig.ValidateIssuer,
                    ValidateLifetime = cognitoConfig.ValidateLifetime,
                    ValidAudience = cognitoConfig.ValidAudience,
                    ValidateAudience = cognitoConfig.ValidateAudience,
                    RoleClaimType = cognitoConfig.RoleClaimType
                };

                options.MetadataAddress = cognitoConfig.MetadataAddress;
            });

        return services;
    }
}
