using ApiPersonProfiles.Core.Domain.Common;

namespace ApiPersonProfiles.Core.Domain;

public class Picture : EntityBase
{
    public string? ThumbnailFileName { get; set; }
    public  string? FileName { get; set; }
    public Profile Profile { get; set; }
    public int ProfileId { get; set; }

}
