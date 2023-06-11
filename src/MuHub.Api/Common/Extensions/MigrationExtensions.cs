using Microsoft.EntityFrameworkCore;

using MuHub.Infrastructure.Persistence;

namespace MuHub.Api.Common.Extensions;

public static class MigrationExtension
{
    public static void ApplyApplicationDbContextMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var pendingMigrations = context.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
        {
            context.Database.Migrate();
        }
    }
}