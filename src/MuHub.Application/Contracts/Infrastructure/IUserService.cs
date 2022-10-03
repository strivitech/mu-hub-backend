using ErrorOr;

using MuHub.Application.Models.Data;
using MuHub.Application.Models.Requests.User;

namespace MuHub.Application.Contracts.Infrastructure;

/// <summary>
/// Provides methods for user management.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Asynchronously creates a new user.
    /// </summary>
    /// <param name="request">Creation model for the user.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// The task result contains either an instance of <see cref="UserDto"/> or errors of type <see cref="Error"/>.</returns>
    Task<ErrorOr<UserDto>> CreateAsync(CreateUserRequest request);
    
    /// <summary>
    /// Asynchronously creates a new user.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// The task result contains either deleted userId or errors of type <see cref="Error"/>.</returns>
    Task<ErrorOr<string>> DeleteAsync(string userId);
}
