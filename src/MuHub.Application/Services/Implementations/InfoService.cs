using AutoMapper;

using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Models.Data;
using MuHub.Application.Models.Requests.Info;
using MuHub.Application.Services.Interfaces;
using MuHub.Domain.Entities;

namespace MuHub.Application.Services.Implementations;

public class InfoService : IInfoService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public InfoService(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<InfoDto> CreateAsync(CreateInfoRequest request)
    {
        // Add fluent validation with null check
        
        var info = _mapper.Map<Info>(request);
        var createdInfo = await _dbContext.Info.AddAsync(info);
        return _mapper.Map<InfoDto>(createdInfo.Entity);
    }
}
