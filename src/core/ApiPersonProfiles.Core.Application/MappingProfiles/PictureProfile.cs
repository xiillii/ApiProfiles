using ApiPersonProfiles.Core.Application.Features.Picture.Queries.GetPicture;
using AutoMapper;

namespace ApiPersonProfiles.Core.Application.MappingProfiles;

public class PictureProfile : Profile
{
    public PictureProfile()
    {
        CreateMap<Domain.Picture, PictureDto>();
    }
}
