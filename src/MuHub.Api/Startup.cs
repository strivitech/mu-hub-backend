using MuHub.Application;
using MuHub.Infrastructure;

namespace MuHub.Api;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddApplicationServices();
        services.AddInfrastructureServices(builder.Configuration);
        services.AddApiServices();
    }

    public static void Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            // app.UseSwagger();
            // app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}
