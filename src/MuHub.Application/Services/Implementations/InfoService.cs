using ErrorOr;
using MuHub.Application.Models.Data;
using MuHub.Application.Models.Errors;
using MuHub.Application.Models.Requests.Info;
using MuHub.Application.Services.Interfaces;

namespace MuHub.Application.Services.Implementations;

public class InfoService : IInfoService
{
    private readonly IModelValidationService _modelValidationService;

    public InfoService(IModelValidationService modelValidationService)
    {
        _modelValidationService = modelValidationService?? throw new ArgumentNullException(nameof(modelValidationService));
    }
    
    public async Task<ErrorOr<InfoDto>> CreateAsync(CreateInfoRequest request)
    {
        if (!await _modelValidationService.CheckIfValidAsync(request))
        {
            return Errors.Info.InvalidInput;
        }

        return new InfoDto()
        {
            Id = 1,
            Subject = "Test",
            Text = "Test"
        };
    }
}
