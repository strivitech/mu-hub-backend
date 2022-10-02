using FluentValidation;

using MuHub.Application.Models.Requests.User;

namespace MuHub.Application.Validation.User;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Login)
            .MinimumLength(3)
            .MaximumLength(10);
        RuleFor(x => x.Password)
            .MinimumLength(8)
            .MaximumLength(30);
        RuleFor(x => x.PhoneNumber)
            .Length(9);
        RuleFor(x => x.RoleName)
            .NotEmpty();
    }
}
