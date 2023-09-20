using ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetAllProfiles;
using AutoMapper;

namespace ApiPersonProfiles.Core.Application.MappingProfiles;

public class ProfileProfile : Profile
{
    public ProfileProfile()
    {
        CreateMap<ProfileDto, Profile>().ReverseMap();
    }
}
