using ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetAllProfiles;
using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Picture.Queries.GetPicture;

public record GetPictureQuery(int ProfileId) : IRequest<PictureDto>;
