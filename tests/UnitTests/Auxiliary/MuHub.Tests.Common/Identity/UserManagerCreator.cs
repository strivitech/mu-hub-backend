using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

namespace MuHub.Tests.Common.Identity;

/// <summary>
/// Contains methods for creating mocks of <see cref="UserManager{TUser}"/> for testing purposes.
/// </summary>
public static class UserManagerCreator
{
    /// <summary>
    /// Creates a mock of <see cref="UserManager{TUser}"/> for testing purposes.
    /// </summary>
    /// <param name="userValidatorCollection">User validators.</param>
    /// <param name="passwordValidatorCollection">Password validators.</param>
    /// <typeparam name="TUser">User type.</typeparam>
    /// <returns>A mock of <see cref="UserManager{TUser}"/>.</returns>
    public static Mock<UserManager<TUser>> CreateMockedUserManager<TUser>(
        IEnumerable<IUserValidator<TUser>>? userValidatorCollection = null,
        IEnumerable<IPasswordValidator<TUser>>? passwordValidatorCollection = null
    )
        where TUser : class
    {
        var userStore = new Mock<IUserStore<TUser>>();
        var identityOptions = new Mock<IOptions<IdentityOptions>>();
        var passwordHasher = new Mock<IPasswordHasher<TUser>>();
        var userValidators = userValidatorCollection ?? Array.Empty<IUserValidator<TUser>>();
        var passwordValidators = passwordValidatorCollection ?? Array.Empty<IPasswordValidator<TUser>>();
        var keyNormalizer = new Mock<ILookupNormalizer>();
        var errors = new Mock<IdentityErrorDescriber>();
        var services = new Mock<IServiceProvider>();
        var logger = new Mock<ILogger<UserManager<TUser>>>();

        return new Mock<UserManager<TUser>>(
            userStore.Object,
            identityOptions.Object,
            passwordHasher.Object,
            userValidators,
            passwordValidators,
            keyNormalizer.Object,
            errors.Object,
            services.Object,
            logger.Object
        );
    }
}
