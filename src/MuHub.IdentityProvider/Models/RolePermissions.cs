namespace MuHub.IdentityProvider.Models;

/// <summary>
/// Role-permissions entity.
/// </summary>
public class RolePermissions
{
    public int Id { get; set; }

    public string RoleName { get; set; }
    
    public string PermissionsString { get; set; }
}
