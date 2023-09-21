using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set;}
    public int Age { get; set; }
    public string? Nickname { get; set; }
}
