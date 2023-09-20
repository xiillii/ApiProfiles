namespace ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetProfileDetails;

public class ProfileDetailsDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string? Nickname { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
}
