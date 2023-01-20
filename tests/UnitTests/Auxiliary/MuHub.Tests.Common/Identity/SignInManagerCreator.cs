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
        var contextAccessor = new Mock<IHttpContextAccessor>();
        var claimsFactory = new Mock<IUserClaimsPrincipalFactory<TUser>>();
        var optionsAccessor = new Mock<IOptions<IdentityOptions>>();
        var logger = new Mock<ILogger<SignInManager<TUser>>>();
        var schemes = new Mock<IAuthenticationSchemeProvider>();
        var confirmation = new Mock<IUserConfirmation<TUser>>();

        return new Mock<SignInManager<TUser>>(
            userManager.Object,
            contextAccessor.Object,
            claimsFactory.Object,
            optionsAccessor.Object,
            logger.Object,
            schemes.Object,
            confirmation.Object);
    }
}
