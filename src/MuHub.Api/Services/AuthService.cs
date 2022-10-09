using ErrorOr;

using Microsoft.AspNetCore.Identity;

using MuHub.Api.ApiModels.Login;
using MuHub.Api.ApiErrors;
using MuHub.Api.ApiModels.Registration;
using MuHub.Application.Contracts.Infrastructure;
using MuHub.Application.Models.Data;
using MuHub.Application.Models.Errors;
using MuHub.Application.Models.Requests.User;
using MuHub.Infrastructure.Identity;
using MuHub.Shared.Constants;
using MuHub.Shared.Constants.Domain;

namespace MuHub.Api.Services;

/// <summary>
/// 
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public AuthService(
        IUserService userService,
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ErrorOr<RegisterUserResponse>> RegisterAsync(RegisterUserRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var createUserRequest = new CreateUserRequest()
        {
            Login = request.Email,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            RoleName = UserConstants.DefaultRoleName
        };
        
        var createUserResult = await _userService.CreateAsync(createUserRequest);
        return createUserResult.Match<ErrorOr<RegisterUserResponse>>(
        x => MapToRegisterUserResponse(x),
        x => x);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ErrorOr<LoginUserResponse>> LoginAsync(LoginUserRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Errors.User.NotFound;
        }
        
        var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
        if (!signInResult.Succeeded)
        {
            if (signInResult.IsLockedOut)
            {
                return Errors.User.IsLockedOut;
            }
            
            return ErrorsScope.Auth.PasswordSignInFailed;
        }
        
        return new LoginUserResponse()
        {
            Id = user.Id,
            Email = user.Email,
            RoleName = user.RoleName
        };
    }

    private static RegisterUserResponse MapToRegisterUserResponse(UserDto userDto)
    {
        return new RegisterUserResponse
        {
            Id = userDto.Id,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            RoleName = userDto.RoleName,
            CreatedAt = userDto.CreatedAt
        };
    }
}
