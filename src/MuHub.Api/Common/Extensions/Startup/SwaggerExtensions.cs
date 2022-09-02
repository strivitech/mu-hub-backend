using Microsoft.Extensions.Options;
using MuHub.Api.Common.Configurations.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MuHub.Api.Common.Extensions.Startup;

/// <summary>
/// Extensions for Swagger setup.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Adds services for Swagger.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(
            options =>
            {
                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();

                var fileName = typeof(Program).Assembly.GetName().Name + ".xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

                // integrate xml comments
                options.IncludeXmlComments(filePath);
            } );

        return services;
    }
    
    /// <summary>
    /// Uses Swagger with API versions.
    /// </summary>
    /// <param name="app">Web Application.</param>
    /// <returns>Web Application.</returns>
    public static WebApplication UseSwaggerWithApiVersioning(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(
            options =>
            {
                var descriptions = app.DescribeApiVersions();

                // build a swagger endpoint for each discovered API version
                foreach (var description in descriptions)
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            } );

        return app;
    }
}
