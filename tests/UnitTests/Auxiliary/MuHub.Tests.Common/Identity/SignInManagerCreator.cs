using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

namespace MuHub.Tests.Common.Identity;

/// <summary>
/// Contains methods for creating mocks of <see cref="SignInManager{TUser}"/> for testing purposes.
/// </summary>
public static class SignInManagerCreator
{
    /// <summary>
    /// Creates a mock of <see cref="SignInManager{TUser}"/> for testing purposes.
    /// </summary>
    /// <typeparam name="TUser">User type.</typeparam>
    /// <returns>A mock of <see cref="SignInManager{TUser}"/>.</returns>
    public static Mock<SignInManager<TUser>> CreateMockedSignInManager<TUser>()
        where TUser : class 
    {
        var userManager = UserManagerCreator.CreateMockedUserManager<TUser>();

        return new Mock<SignInManager<TUser>>(
            userManager.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<TUser>>(),
            Mock.Of<IOptions<IdentityOptions>>(),
            Mock.Of<ILogger<SignInManager<TUser>>>(),
            Mock.Of<IAuthenticationSchemeProvider>(),
            Mock.Of<IUserConfirmation<TUser>>());
    }
}
