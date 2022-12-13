using AutoMapper;

using MuHub.Api.Requests;
using MuHub.Application.Models.Responses;
using MuHub.Domain.Entities;

namespace MuHub.Api.Mappings;

/// <summary>
/// 
/// </summary>
public class UserMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public UserMappingProfile()
    {
        CreateMap<GetIdentityProviderUserResponse, User>()
            .ForMember(x => x.Id, opt => opt.Ignore());
    }
}
