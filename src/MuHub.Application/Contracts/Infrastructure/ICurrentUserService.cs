namespace MuHub.Application.Contracts.Infrastructure;

public interface ICurrentUserService
{
    IUserSessionData UserSessionData { get; }
}
