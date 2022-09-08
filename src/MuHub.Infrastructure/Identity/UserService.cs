using System.Collections.Immutable;
using System.Diagnostics;

using LanguageExt;
using LanguageExt.Common;

using Microsoft.AspNetCore.Identity;

using MuHub.Application.Contracts.Persistence;
using MuHub.Infrastructure.Contracts.Data;
using MuHub.Infrastructure.Contracts.Requests;

namespace MuHub.Infrastructure.Identity;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IApplicationDbContext _applicationDbContext;

    public UserService(UserManager<User> userManager, IApplicationDbContext applicationDbContext)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
    }

    public async Task<Either<IEnumerable<IdentityError>, UserDto>> CreateUserAsync(CreateUserRequest request)
    {
        // Add Validator

        var user = MapCreateUserRequestToUser(request);
        try
        {
            await using var transaction = await _applicationDbContext.Instance.Database.BeginTransactionAsync();
            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                return createResult.Errors.ToArray();
            }
            var addToRoleResult = await _userManager.AddToRoleAsync(user, request.RoleName);
            if (!addToRoleResult.Succeeded)
            {
                return addToRoleResult.Errors.ToArray();
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

    private User MapCreateUserRequestToUser(CreateUserRequest request)
    {
        return new User()
        {
            UserName = request.Login,
            Email = request.Login,
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTimeOffset.UtcNow,
            LastLoginAt = DateTimeOffset.UtcNow, // Remove this one and create another migration
            RoleName = request.RoleName,
            IsBlocked = false,
        };
    }
    
    private UserDto MapUserToUserDto(User request)
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
