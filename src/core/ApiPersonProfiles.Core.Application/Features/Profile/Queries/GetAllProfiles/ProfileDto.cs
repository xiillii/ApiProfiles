namespace ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetAllProfiles;

public class ProfileDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string? Nickname { get; set; }
}
