using LanguageExt;

using Microsoft.AspNetCore.Identity;

using MuHub.Api.Contracts.Requests;
using MuHub.Infrastructure.Contracts.Data;

namespace MuHub.Api.Services;

public interface IAuthService
{
    Task<Either<IEnumerable<IdentityError>, UserDto>> RegisterAsync(RegisterUserRequest request);
}
