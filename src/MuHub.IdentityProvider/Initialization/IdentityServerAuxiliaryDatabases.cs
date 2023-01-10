using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;

using MuHub.IdentityProvider.Configurations.Clients;

using Serilog;

namespace MuHub.IdentityProvider.Initialization;

public static class IdentityServerAuxiliaryDatabases
{
    public static void InitializeConfigurationDatabase(this IApplicationBuilder app, IConfiguration configuration)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        var transaction = context.Database.BeginTransaction();
        try
        {
            if (!context.Clients.Any())
            {
                var clients = configuration.GetSection(IdentityServerClientsConfiguration.SectionName)
                    .Get<IdentityServerClientsConfiguration>();

                // Add CodeWithPkceClients
                AddClients(context, Config.CodeWithPkceClients(clients.CodeWithPkce));
            }

            if (!context.IdentityResources.Any())
            {
                AddIdentityResources(context);
            }

            if (!context.ApiScopes.Any())
            {
                AddApiScopes(context);
            }

            transaction.Commit();
        }
        catch (Exception)
        {
            Log.Error(
                "Unable to initialize Configuration database, exception was caught. Transaction is getting rollback");
            transaction.Rollback();
            throw;
        }
    }

    private static void AddClients(ConfigurationDbContext context, IEnumerable<Client> clients)
    {
        foreach (var client in clients)
        {
            context.Clients.Add(client.ToEntity());
        }

        context.SaveChanges();
    }

    private static void AddIdentityResources(ConfigurationDbContext context)
    {
        foreach (var resource in Config.IdentityResources)
        {
            context.IdentityResources.Add(resource.ToEntity());
        }

        context.SaveChanges();
    }

    private static void AddApiScopes(ConfigurationDbContext context)
    {
        foreach (var resource in Config.ApiScopes)
        {
            context.ApiScopes.Add(resource.ToEntity());
        }

        context.SaveChanges();
    }
}
