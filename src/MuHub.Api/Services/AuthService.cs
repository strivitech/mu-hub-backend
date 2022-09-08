using LanguageExt;

using Microsoft.AspNetCore.Identity;

using MuHub.Api.Contracts.Requests;
using MuHub.Infrastructure.Contracts.Data;
using MuHub.Infrastructure.Contracts.Requests;
using MuHub.Infrastructure.Identity;

namespace MuHub.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    
    public AuthService(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }
    
    public async Task<Either<IEnumerable<IdentityError>, UserDto>> RegisterAsync(RegisterUserRequest request)
    {
        var createUserRequest = new CreateUserRequest
        {
            Login = request.Login, Password = request.Password, PhoneNumber = request.PhoneNumber, RoleName = "user"
        };

        return await _userService.CreateUserAsync(createUserRequest);
    }
}
