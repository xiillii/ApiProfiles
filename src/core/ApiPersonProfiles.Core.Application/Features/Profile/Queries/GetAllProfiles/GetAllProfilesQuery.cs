using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetAllProfiles;

public record GetAllProfilesQuery : IRequest<List<ProfileDto>>;

