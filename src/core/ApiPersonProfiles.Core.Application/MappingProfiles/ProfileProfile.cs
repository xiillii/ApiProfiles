using ApiPersonProfiles.Core.Application.Features.Profile.Commands.CreateProfile;
using ApiPersonProfiles.Core.Application.Features.Profile.Commands.UpdateProfile;
using ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetAllProfiles;
using ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetProfileDetails;
using AutoMapper;

namespace ApiPersonProfiles.Core.Application.MappingProfiles;

public class ProfileProfile : Profile
{
    public ProfileProfile()
    {
        CreateMap<ProfileDto, Domain.Profile>().ReverseMap();
        CreateMap<Domain.Profile, ProfileDetailsDto>();

        CreateMap<CreateProfileCommand, Domain.Profile>();
        CreateMap<UpdateProfileCommand, Domain.Profile>();
    }
}
