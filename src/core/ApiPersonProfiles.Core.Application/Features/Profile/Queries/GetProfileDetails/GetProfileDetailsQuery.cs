using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetProfileDetails;

public record GetProfileDetailsQuery(int Id) : IRequest<ProfileDetailsDto>;
