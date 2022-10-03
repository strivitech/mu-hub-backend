using ErrorOr;

namespace MuHub.Application.Models.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error NotFound => Error.NotFound("User.NotFound", "User was not found");
        public static Error AlreadyExists => Error.Conflict("User.AlreadyExists", "User already exists");
        public static Error NotActive => Error.Failure("User.NotActive", "User is not active");
        public static Error CreationFailed => Error.Failure("User.CreationFailed", "User was not created");
        public static Error AddToRoleFailed => Error.Failure("User.AddToRoleFailed", "User was not added to role");
        public static Error DeletionFailed => Error.Failure("User.DeletionFailed", "User was not deleted");
        public static Error NotConfirmed => Error.Failure("User.NotConfirmed", "User is not confirmed");
        public static Error IsLockedOut => Error.Failure("User.IsLockedOut", "User is locked out");
        public static Error IsDeleted => Error.Failure("User.IsDeleted", "User is deleted");
        public static Error IsDisabled => Error.Failure("User.IsDisabled", "User is disabled");
        public static Error IsNotInRole => Error.Failure("User.IsNotInRole", "User is not in role");
        public static Error IsInRole => Error.Failure("User.IsInRole", "User is in role");
        public static Error IsNotInAnyRole => Error.Failure("User.IsNotInAnyRole", "User is not in any role");
        public static Error IsInAnyRole => Error.Failure("User.IsInAnyRole", "User is in any role");
        public static Error IsNotInAllRoles => Error.Failure("User.IsNotInAllRoles", "User is not in all roles");
        public static Error IsInAllRoles => Error.Failure("User.IsInAllRoles", "User is in all roles");
        public static Error IsNotInAnyOfRoles => Error.Failure("User.IsNotInAnyOfRoles", "User is not in any of roles");
        public static Error IsInAnyOfRoles => Error.Failure("User.IsInAnyOfRoles", "User is in any of roles");
        public static Error IsNotInAllOfRoles => Error.Failure("User.IsNotInAllOfRoles", "User is not in all of roles");
        public static Error IsInAllOfRoles => Error.Failure("User.IsInAllOfRoles", "User is in all of roles");
        public static Error IsNotInAnyOfRolesList => Error.Failure("User.IsNotInAnyOfRolesList", "User is not in any of roles list");
        public static Error IsInAnyOfRolesList => Error.Failure("User.IsInAnyOfRolesList", "User is in any of roles list");
    }
}

