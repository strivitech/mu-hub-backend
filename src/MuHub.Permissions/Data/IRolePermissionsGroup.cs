namespace MuHub.Permissions.Data;

/// <summary>
/// Represents a role-permissions group.
/// </summary>
public interface IRolePermissionsGroup
{
    /// <summary>
    /// Role.
    /// </summary>
    Role Role { get; }
    
    /// <summary>
    /// Permissions.
    /// </summary>
    IReadOnlyCollection<Permission> Permissions { get; }
}
