namespace ApiPersonProfiles.Core.Domain.Common;

public abstract class EntityBase
{
    public int Id { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
}
