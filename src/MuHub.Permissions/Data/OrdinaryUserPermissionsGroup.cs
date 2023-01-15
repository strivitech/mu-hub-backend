﻿namespace MuHub.Permissions.Data;

public class OrdinaryUserPermissionsGroup : IRolePermissionsGroup
{
    private static readonly IReadOnlyCollection<Permission> PermissionsCollection = new List<Permission>
    {
        Permission.InfoRead,
    }.AsReadOnly();

    public Role Role => Role.OrdinaryUser;
    public IReadOnlyCollection<Permission> Permissions => PermissionsCollection;
}
