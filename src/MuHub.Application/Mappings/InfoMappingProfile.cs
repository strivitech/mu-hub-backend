using AutoMapper;

using MuHub.Application.Models.Requests.Info;
using MuHub.Domain.Entities;

namespace MuHub.Application.Mappings;

public class InfoMappingProfile : Profile
{
    public InfoMappingProfile()
    {
        CreateMap<CreateInfoRequest, Info>();
        CreateMap<UpdateInfoRequest, Info>();
    }
}
