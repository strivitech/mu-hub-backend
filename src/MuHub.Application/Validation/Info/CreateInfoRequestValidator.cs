using FluentValidation;

using MuHub.Application.Models.Requests.Info;

namespace MuHub.Application.Validation.Info;

/// <summary>
/// Validates an instance of <see cref="CreateInfoRequest"/> model.
/// </summary>
public class CreateInfoRequestValidator : AbstractValidator<CreateInfoRequest>
{
    public CreateInfoRequestValidator()
    {
        RuleFor(x => x.Subject)
            .MinimumLength(3)
            .MaximumLength(10);
        RuleFor(x => x.Text)
            .NotEmpty()
            .MaximumLength(30);
    }
}
