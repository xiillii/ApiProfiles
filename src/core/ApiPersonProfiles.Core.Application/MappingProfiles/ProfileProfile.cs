using ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetAllProfiles;
using ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetProfileDetails;
using AutoMapper;

namespace ApiPersonProfiles.Core.Application.MappingProfiles;

public class ProfileProfile : Profile
{
    public ProfileProfile()
    {
        CreateMap<ProfileDto, Profile>().ReverseMap();
        CreateMap<Profile, ProfileDetailsDto>();
    }
}
