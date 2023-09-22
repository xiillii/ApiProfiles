using ApiPersonProfiles.Core.Application.Features.Picture.Commands.UploadPicture;
using ApiPersonProfiles.Core.Application.Features.Picture.Queries.GetPicture;
using AutoMapper;

namespace ApiPersonProfiles.Core.Application.MappingProfiles;

public class PictureProfile : Profile
{
    public PictureProfile()
    {
        CreateMap<Domain.Picture, PictureDto>();

        CreateMap<UploadPictureCommand, Domain.Picture>();
    }
}
