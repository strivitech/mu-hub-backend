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
        var userValidators = userValidatorCollection ?? Array.Empty<IUserValidator<TUser>>();
        var passwordValidators = passwordValidatorCollection ?? Array.Empty<IPasswordValidator<TUser>>();

        return new Mock<UserManager<TUser>>(
            Mock.Of<IUserStore<TUser>>(),
            Mock.Of<IOptions<IdentityOptions>>(),
            Mock.Of<IPasswordHasher<TUser>>(),
            userValidators,
            passwordValidators,
            Mock.Of<ILookupNormalizer>(),
            Mock.Of<IdentityErrorDescriber>(),
            Mock.Of<IServiceProvider>(),
            Mock.Of<ILogger<UserManager<TUser>>>()
        );
    }
}
