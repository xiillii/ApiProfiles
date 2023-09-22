using ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetAllProfiles;

namespace ApiPersonProfiles.Core.Application.Features.Picture.Queries.GetPicture;

public class PictureDto
{
    public int Id { get; set; }
    public string? ThumbnailFileName { get; set; }
    public string? FileName { get; set; }
    public ProfileDto? Profile { get; set; }
    public int ProfileId { get; set; }
}
