using Cognito.Common;

using FluentValidation;

using MuHub.Api.Requests;
using MuHub.SharedData.Domain;

namespace MuHub.Api.Validations;

/// <summary>
/// 
/// </summary>
public class UserLinkRegistrationRequestValidator : AbstractValidator<UserLinkRegistrationDataRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public UserLinkRegistrationRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty();

        RuleFor(x => x.CreatedAt)
            .NotNull()
            .LessThanOrEqualTo(DateTimeOffset.UtcNow)
            .GreaterThanOrEqualTo(DateTimeOffset.UtcNow.AddMinutes(-UserRegistration.MaxRegistrationDelayDays));
    }
}
