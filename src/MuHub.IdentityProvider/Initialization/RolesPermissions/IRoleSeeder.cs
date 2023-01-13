namespace MuHub.IdentityProvider.Initialization.RolesPermissions;

public interface IRoleSeeder
{
    IPermissionsSeeder AddRoles(IApplicationBuilder app);
}
