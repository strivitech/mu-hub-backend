using FluentValidation;

using MuHub.Application.Models.Responses;
using MuHub.SharedData.Domain;

namespace MuHub.Api.Validations;

/// <summary>
/// 
/// </summary>
public class GetIdentityProviderUserResponseValidator : AbstractValidator<GetIdentityProviderUserResponse>
{
    /// <summary>
    /// 
    /// </summary>
    public GetIdentityProviderUserResponseValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();
        
        RuleFor(x => x.UserName)
            .NotEmpty();

        RuleFor(x => x.PhoneNumber)
            .Matches(UserConstants.PhoneNumberRegex);

        RuleFor(x => x.CreatedAt)
            .NotNull();
        
        RuleFor(x => x.EmailConfirmed)
            .Equal(true);
        
        RuleFor(x => x.PhoneNumberConfirmed)
            .Equal(false);
    }
}
