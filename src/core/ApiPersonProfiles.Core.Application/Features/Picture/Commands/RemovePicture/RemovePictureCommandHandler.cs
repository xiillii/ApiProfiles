using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Picture.Commands.RemovePicture;

public class RemovePictureCommandHandler : IRequestHandler<RemovePictureCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IProfileRepository _profileRepository;
    private readonly IPictureRepository _pictureRepository;

    public RemovePictureCommandHandler(IMapper mapper
        , IProfileRepository profRepo
        , IPictureRepository pictRepo)
    {
        _mapper = mapper;
        _profileRepository = profRepo;
        _pictureRepository = pictRepo;
    }

    public async Task<Unit> Handle(RemovePictureCommand request, CancellationToken cancellationToken)
    {
        // validate if profile exists
        var profile = await _profileRepository.GetByIdAsync(request.ProfileId);
        if (profile == null)
        {
            throw new NotFoundException(nameof(Domain.Profile), request.ProfileId);
        }

        // get the picture, if exist delete it
        var picture = await _pictureRepository.GetByProfileIdAsync(request.ProfileId);

        if (picture != null)
        {
            await _pictureRepository.DeleteAsync(picture);
        }

        // return nothing
        return Unit.Value;
    }
}
