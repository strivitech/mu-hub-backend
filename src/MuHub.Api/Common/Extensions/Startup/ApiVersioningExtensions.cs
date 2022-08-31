using Asp.Versioning;
using Asp.Versioning.Routing;

namespace MuHub.Api.Common.Extensions.Startup;

/// <summary>
/// Extensions for API versioning.
/// </summary>
public static class ApiVersioningExtensions
{
    /// <summary>
    /// Adds versioning for API controllers.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddApiControllersVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(
                options =>
                {
                    // Reporting api versions will return the headers
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                })
            .AddMvc()
            .AddApiExplorer(
                options =>
                {
                    // Add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // Can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

        // This hides a warning "route parameter constraint apiVersion not resolved"
#pragma warning disable IL2026
        new RouteOptions().ConstraintMap.Add("apiVersion", typeof(ApiVersionRouteConstraint));
#pragma warning restore IL2026
        
        return services;
    }
}
