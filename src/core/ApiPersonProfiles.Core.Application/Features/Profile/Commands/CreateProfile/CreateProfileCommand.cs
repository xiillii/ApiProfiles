using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Commands.CreateProfile;

public class CreateProfileCommand :IRequest<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string? MyProperty { get; set; } = string.Empty;
}
