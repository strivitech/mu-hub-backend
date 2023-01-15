using Microsoft.AspNetCore.Identity;

using MuHub.IdentityProvider.Data;
using MuHub.IdentityProvider.Models;
using MuHub.Permissions;
using MuHub.Permissions.Management;

using Serilog;

namespace MuHub.IdentityProvider.Initialization.RolesPermissions;

/// <summary>
/// Seeds roles and permissions.
/// </summary>
public static class RolesPermissionsSeeder
{
    private class InsideStepwiseSeeder :
        IRoleSeeder,
        IPermissionsSeeder
    {
        public IPermissionsSeeder AddRoles(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var roles = typeof(Role).GetEnumNames();
            var transaction = applicationDbContext.Database.BeginTransaction();
            try
            {
                foreach (var role in roles)
                {
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        var result = roleManager.CreateAsync(new IdentityRole(role)).Result;
                        if (!result.Succeeded)
                        {
                            Log.Error("Error while creating role {Role}: {@Result}", role, result);
                            throw new InvalidOperationException("Error while creating role");
                        }
                    }
                }

                transaction.Commit();
                Log.Information("Roles added");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while adding roles");
                transaction.Rollback();
                throw;
            }

            return this;
        }
        
        public void AddPermissions(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var rolesPermissions = RolesPermissionsStaticReceiver.GetAllRolePermissionsGroups;
            var transaction = applicationDbContext.Database.BeginTransaction();
            try
            {
                foreach (var value in rolesPermissions.Values.Where(value =>
                             !applicationDbContext.RolePermissions.Any(x => x.RoleName == value.Role.ToString())))
                {
                    if (!value.Permissions.Any())
                    {
                        Log.Warning("Role {RoleName} has no permissions", value.Role.ToString());
                    }

                    string permissionsString = value.Permissions.CreateIntoString();
                    var role = applicationDbContext.Roles.First(r => r.Name == value.Role.ToString());

                    applicationDbContext.RolePermissions.Add(
                        new RolePermissions { RoleName = role.Name, PermissionsString = permissionsString });
                }

                applicationDbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while adding roles");
                transaction.Rollback();
                throw;
            }
        }
    }

    /// <summary>
    /// Creates a new instance of <see cref="RolesPermissionsSeeder"/>.
    /// </summary>
    /// <returns>An instance of <see cref="IRoleSeeder"/>.</returns>
    public static IRoleSeeder Create() => new InsideStepwiseSeeder();
}
