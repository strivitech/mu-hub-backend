#if DEBUG

using System.Security.Claims;
using IdentityModel;

using Microsoft.AspNetCore.Identity;

using MuHub.IdentityProvider.Data;
using MuHub.IdentityProvider.Models;

using Serilog;

namespace MuHub.IdentityProvider.Initialization;

/// <summary>
/// Seed development data.
/// </summary>
public static class SeedDevelopmentData
{
    public static void TrySeedDevelopmentItems(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (context.Users.Any())
        {
            return;
        }

        var transaction = context.Database.BeginTransaction();
        try
        {
            CreateUsersForDevelopment(userMgr);
            transaction.Commit();
        }
        catch (Exception)
        {
            Log.Error("Unable to seed users, exception was caught. Transaction is getting rollback");
            transaction.Rollback();
            throw;
        }
    }
    
    private static void CreateUsersForDevelopment(UserManager<ApplicationUser> userMgr)
    {
        var alice = userMgr.FindByNameAsync("alice").Result;
        if (alice is null)
        {
            alice = new ApplicationUser { UserName = "alice", Email = "AliceSmith@email.com", EmailConfirmed = true, };
            var result = userMgr.CreateAsync(alice, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(alice,
                new Claim[]
                {
                    new(JwtClaimTypes.Name, "Alice Smith"), new(JwtClaimTypes.GivenName, "Alice"),
                    new(JwtClaimTypes.FamilyName, "Smith"), new(JwtClaimTypes.WebSite, "http://alice.com"),
                }).Result;
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }

            Log.Debug("Alice was created");
        }
        else
        {
            Log.Debug("Alice already exists");
        }

        var bob = userMgr.FindByNameAsync("bob").Result;
        if (bob is null)
        {
            bob = new ApplicationUser { UserName = "bob", Email = "BobSmith@email.com", EmailConfirmed = true };
            var result = userMgr.CreateAsync(bob, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(bob,
                new Claim[]
                {
                    new(JwtClaimTypes.Name, "Bob Smith"), new(JwtClaimTypes.GivenName, "Bob"),
                    new(JwtClaimTypes.FamilyName, "Smith"), new(JwtClaimTypes.WebSite, "http://bob.com"),
                    new("location", "somewhere")
                }).Result;
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.First().Description);
            }

            Log.Debug("Bob was created");
        }
        else
        {
            Log.Debug("Bob already exists");
        }
    }
}

#endif
