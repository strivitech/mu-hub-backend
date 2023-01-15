using Duende.IdentityServer.EntityFramework.DbContexts;

using Microsoft.EntityFrameworkCore;

using MuHub.IdentityProvider.Data;

using Serilog;

namespace MuHub.IdentityProvider.Initialization;

/// <summary>
/// Contains methods to manage databases that are called before app runs.
/// </summary>
public static class DbContexts
{
    /// <summary>
    /// Migrates all databases to last versions.
    /// </summary>
    /// <param name="app"></param>
    public static void Migrate(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
        var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            configurationDbContext.Database.Migrate();
            persistedGrantDbContext.Database.Migrate();
            applicationDbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while migrating the databases");
            throw;
        }
    }
}
