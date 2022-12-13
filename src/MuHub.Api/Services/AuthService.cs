using Amazon.CognitoIdentityProvider;

using AutoMapper;

using MuHub.Api.Requests;
using MuHub.Api.Responses;
using MuHub.Application.Contracts.Infrastructure;
using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Services.Interfaces;
using MuHub.Domain.Entities;

namespace MuHub.Api.Services;

/// <summary>
/// 
/// </summary>
public class AuthService : IAuthService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IIdentityProvider _identityProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IModelValidationService _modelValidationService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="identityProvider"></param>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
    /// <param name="modelValidationService"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public AuthService(IApplicationDbContext dbContext, IIdentityProvider identityProvider, IMapper mapper,
        ILogger logger, IModelValidationService modelValidationService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _identityProvider = identityProvider ?? throw new ArgumentNullException(nameof(identityProvider));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _modelValidationService = modelValidationService ?? throw new ArgumentNullException(nameof(modelValidationService));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<UserLinkRegistrationDataResponse?> RelateUserRegistrationFlowAsync(
        UserLinkRegistrationDataRequest request)
    {
        var user = _dbContext.Users
            .FirstOrDefault(x => x.UserName == request.UserName);
        if (user is not null)
        {
            return new UserLinkRegistrationDataResponse { ApplicationUserId = user.Id };
        }

        var identityUser = await _identityProvider.GetIdentityProviderUserAsync(request.UserName);
        var identityUserValid = await _modelValidationService.CheckIfValidAsync(identityUser);
        if (!identityUserValid)
        {
            return null;
        }

        var newUserEntry = await _dbContext.Users.AddAsync(_mapper.Map<User>(identityUser));
        await _dbContext.SaveChangesAsync();
        return new UserLinkRegistrationDataResponse { ApplicationUserId = newUserEntry.Entity.Id };
    }
}
