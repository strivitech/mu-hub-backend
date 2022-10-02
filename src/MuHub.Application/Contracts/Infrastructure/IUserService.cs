using ErrorOr;

using MuHub.Application.Models.Data;
using MuHub.Application.Models.Requests.User;

namespace MuHub.Application.Contracts.Infrastructure;

public interface IUserService
{
    Task<ErrorOr<UserDto>> CreateUserAsync(CreateUserRequest request);
}
