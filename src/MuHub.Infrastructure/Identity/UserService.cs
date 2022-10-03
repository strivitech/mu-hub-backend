using System.Diagnostics;

using ErrorOr;

using Microsoft.AspNetCore.Identity;

using MuHub.Application.Contracts.Infrastructure;
using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Models.Data;
using MuHub.Application.Models.Errors;
using MuHub.Application.Models.Requests.User;
using MuHub.Application.Services.Interfaces;

namespace MuHub.Infrastructure.Identity;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _applicationDbContext;

    public UserService(
        UserManager<User> userManager,
        IApplicationDbContext applicationDbContext)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
    }

    public async Task<ErrorOr<UserDto>> CreateAsync(CreateUserRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var user = MapCreateUserRequestToUser(request);
        try
        {
            await using var transaction = await _applicationDbContext.Instance.Database.BeginTransactionAsync();
            
            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                await transaction.RollbackAsync();
                return Errors.User.CreationFailed;
            }
            
            var addToRoleResult = await _userManager.AddToRoleAsync(user, request.RoleName);
            if (!addToRoleResult.Succeeded)
            {
                await transaction.RollbackAsync();
                return Errors.User.AddToRoleFailed;
            }

            await transaction.CommitAsync();
            return MapUserToUserDto(user);
        }
        catch (Exception ex)
        {
            // Add logging
            Debug.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<ErrorOr<string>> DeleteAsync(string userId)
    {
        ArgumentNullException.ThrowIfNull(userId);
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return Errors.User.NotFound;
        }
        
        var deletionResult = await _userManager.DeleteAsync(user);
        if (!deletionResult.Succeeded)
        {
            return Errors.User.DeletionFailed;
        }
        
        return userId;
    }
    
    private static User MapCreateUserRequestToUser(CreateUserRequest request)
    {
        return new User()
        {
            UserName = request.Login,
            Email = request.Login,
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTimeOffset.UtcNow,
            RoleName = request.RoleName,
            IsBlocked = false,
        };
    }

    private static UserDto MapUserToUserDto(User request)
    {
        return new UserDto
        {
            Id = request.Id,
            UserName = request.UserName,
            NormalizedUserName = request.NormalizedUserName,
            Email = request.Email,
            NormalizedEmail = request.NormalizedEmail,
            EmailConfirmed = request.EmailConfirmed,
            PasswordHash = request.PasswordHash,
            SecurityStamp = request.SecurityStamp,
            ConcurrencyStamp = request.ConcurrencyStamp,
            PhoneNumber = request.PhoneNumber,
            PhoneNumberConfirmed = request.PhoneNumberConfirmed,
            TwoFactorEnabled = request.TwoFactorEnabled,
            LockoutEnd = request.LockoutEnd,
            LockoutEnabled = request.LockoutEnabled,
            AccessFailedCount = request.AccessFailedCount,
            IsBlocked = request.IsBlocked,
            RoleName = request.RoleName,
            CreatedAt = request.CreatedAt,
            LastLoginAt = request.LastLoginAt
        };
    }
}
