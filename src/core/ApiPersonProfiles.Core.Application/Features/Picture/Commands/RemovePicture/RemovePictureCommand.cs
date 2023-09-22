using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Picture.Commands.RemovePicture;

public class RemovePictureCommand : IRequest<Unit>
{
    public int ProfileId { get; set; }
}
