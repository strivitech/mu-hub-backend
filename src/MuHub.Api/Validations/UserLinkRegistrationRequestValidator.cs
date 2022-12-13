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
    }
}
