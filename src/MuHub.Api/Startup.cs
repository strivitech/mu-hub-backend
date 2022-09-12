using MuHub.Api.Common.Extensions.Startup;
using MuHub.Application;
using MuHub.Infrastructure;

namespace MuHub.Api;

/// <summary>
/// Contains methods to set up essential dependencies and pipelines.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Configures services for the application.
    /// </summary>
    /// <param name="builder">Web Application.</param>
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddApplicationServices();
        services.AddInfrastructureServices(builder.Configuration);
        services.AddApiServices();
    }

    /// <summary>
    /// Configures the middleware pipeline for the application.
    /// </summary>
    /// <param name="app">Web Application.</param>
    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            // app.UseSwagger();
            // app.UseSwaggerUI();
        }

        app.UseExceptionHandler("/error");
        
        app.UseSwaggerWithApiVersioning();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}
