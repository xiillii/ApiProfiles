using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Commands.DeleteProfile;

public class DeleteProfileCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
