namespace MuHub.Permissions.Data;

/// <summary>
/// Admin permissions group.
/// </summary>
public class AdminPermissionsGroup : IRolePermissionsGroup
{
    private static readonly IReadOnlyCollection<Permission> PermissionsCollection = new List<Permission>
    {
        Permission.InfoRead, Permission.InfoCreate, Permission.InfoUpdate, Permission.InfoDelete,
    }.AsReadOnly();

    public Role Role => Role.Admin;
    public IReadOnlyCollection<Permission> Permissions => PermissionsCollection;
}
