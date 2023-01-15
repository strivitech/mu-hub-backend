namespace MuHub.IdentityProvider.Initialization.RolesPermissions;

/// <summary>
/// Permissions seeder.
/// </summary>
public interface IPermissionsSeeder
{
    /// <summary>
    /// Adds the permissions to this application.
    /// </summary>
    /// <param name="app">Application builder.</param>
    void AddPermissions(IApplicationBuilder app);
}
