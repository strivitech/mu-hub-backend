namespace MuHub.IdentityProvider.Initialization.RolesPermissions;

/// <summary>
/// Role seeder.
/// </summary>
public interface IRoleSeeder
{
    /// <summary>
    /// Adds the roles to this application.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <returns>An instance of <see cref="IPermissionsSeeder"/>.</returns>
    IPermissionsSeeder AddRoles(IApplicationBuilder app);
}
