using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Picture.Commands.UploadPicture;

public class UploadPictureCommand : IRequest<int>
{
    public string? ThumbnailFileName { get; set; }
    public string? FileName { get; set; }
    public int ProfileId { get; set; }
}
