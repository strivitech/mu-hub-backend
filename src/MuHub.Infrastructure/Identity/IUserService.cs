using LanguageExt;
using LanguageExt.Common;

using Microsoft.AspNetCore.Identity;

using MuHub.Infrastructure.Contracts.Data;
using MuHub.Infrastructure.Contracts.Requests;

namespace MuHub.Infrastructure.Identity;

public interface IUserService
{
    Task<Either<IEnumerable<IdentityError>, UserDto>> CreateUserAsync(CreateUserRequest request);
}
