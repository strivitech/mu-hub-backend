using FluentValidation;

using MuHub.Application.Models.Data.WatchList;

namespace MuHub.Application.Validation.WatchList;

public class AddToWatchListRequestValidator : AbstractValidator<AddToWatchListRequest>
{
    public AddToWatchListRequestValidator()
    {
        RuleFor(x => x.CoinId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("CoinId must be greater than or equal to 1.");
    }
}
