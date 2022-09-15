using AutoMapper;

using ErrorOr;
using MuHub.Application.Models.Data;
using MuHub.Application.Models.Errors;
using MuHub.Application.Models.Requests.Info;
using MuHub.Application.Services.Interfaces;
using MuHub.Domain.Entities;

namespace MuHub.Application.Services.Implementations;

public class InfoService : IInfoService
{
    private readonly IMapper _mapper;
    private readonly IModelValidationService _modelValidationService;

    public InfoService(IMapper mapper, IModelValidationService modelValidationService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _modelValidationService = modelValidationService?? throw new ArgumentNullException(nameof(modelValidationService));
    }
    
    public async Task<ErrorOr<InfoDto>> CreateAsync(CreateInfoRequest request)
    {
        if (!await _modelValidationService.CheckIfValidAsync(request))
        {
            return Errors.Info.InvalidInput;
        }

        var info = _mapper.Map<Info>(request);
        // var createdInfo = await _dbContext.Info.AddAsync(info);
        // return _mapper.Map<InfoDto>(createdInfo.Entity);
        return _mapper.Map<InfoDto>(info);
    }
}
