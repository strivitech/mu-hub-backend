namespace MuHub.Permissions.Data;

public interface IRolePermissionsGroup
{
    Role Role { get; }
    IReadOnlyCollection<Permission> Permissions { get; }
}
