using FluentValidation;

using MuHub.Shared.Constants;
using MuHub.Shared.Constants.Domain;

namespace MuHub.Api.ApiModels.Registration;

/// <summary>
/// Validator for <see cref="RegisterUserRequest"/>
/// </summary>
public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterUserRequestValidator"/> class.
    /// </summary>
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(UserConstants.LoginLength);
        
        RuleFor(x => x.Password)
            .Matches(UserConstants.PasswordRegex);
        
        RuleFor(x => x.PhoneNumber)
            .Matches(UserConstants.PhoneNumberRegex);
        
        RuleFor(x => x.RepeatPassword)
            .Equal(x => x.Password)
            .WithMessage("Passwords are not equal");
    }
}
