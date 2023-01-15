namespace MuHub.Permissions.Data;

/// <summary>
/// Moderator permissions group.
/// </summary>
public class ModeratorPermissionsGroup : IRolePermissionsGroup
{
    private static readonly IReadOnlyCollection<Permission> PermissionsCollection = new List<Permission>
    {
        Permission.InfoRead, Permission.InfoCreate, Permission.InfoUpdate, Permission.InfoDelete,
    }.AsReadOnly();

    public Role Role => Role.Moderator;
    public IReadOnlyCollection<Permission> Permissions => PermissionsCollection;
}
