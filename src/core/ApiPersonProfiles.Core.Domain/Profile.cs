using ApiPersonProfiles.Core.Domain.Common;

namespace ApiPersonProfiles.Core.Domain;

public class Profile : EntityBase
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string? Nickname { get; set; }
}
