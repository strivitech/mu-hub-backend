namespace MuHub.IdentityProvider.Models;

public class RolePermissions
{
    public int Id { get; set; }

    public string RoleName { get; set; }
    
    public string PermissionsString { get; set; }
}
