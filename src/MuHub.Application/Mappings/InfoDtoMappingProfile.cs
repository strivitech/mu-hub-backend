using AutoMapper;

using MuHub.Application.Models.Data;
using MuHub.Domain.Entities;

namespace MuHub.Application.Mappings;

public class InfoDtoMappingProfile : Profile
{
    public InfoDtoMappingProfile()
    {
        CreateMap<Info, InfoDto>();
    }
}
