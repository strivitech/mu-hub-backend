namespace MuHub.IdentityProvider.Initialization.RolesPermissions;

public interface IPermissionsSeeder
{
    void AddPermissions(IApplicationBuilder app);
}
