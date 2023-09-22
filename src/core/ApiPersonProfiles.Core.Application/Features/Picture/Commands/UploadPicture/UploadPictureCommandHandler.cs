using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Picture.Commands.UploadPicture;

public class UploadPictureCommandHandler : IRequestHandler<UploadPictureCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IProfileRepository _profileRepository;
    private readonly IPictureRepository _pictureRepository;

    public UploadPictureCommandHandler(IMapper mapper
        , IProfileRepository profRepo
        , IPictureRepository pictRepo)
    {
        _mapper = mapper;
        _profileRepository = profRepo;
        _pictureRepository = pictRepo;
    }

    public async Task<int> Handle(UploadPictureCommand request, CancellationToken cancellationToken)
    {
        // validate if profile exists
        var profile = await _profileRepository.GetByIdAsync(request.ProfileId);
        if (profile == null) 
        {
            throw new NotFoundException(nameof(Domain.Profile), request.ProfileId);
        }

        // Validate incoming data
        var validator = new UploadPictureCommandValidator(_profileRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Picture", validationResult);
        }

        // if the profile id exists, we have to update it, else, insert the data
        var picture = await _pictureRepository.GetByProfileIdAsync(request.ProfileId);
        if (picture == null)
        {
            var pictureToCreate = _mapper.Map<Domain.Picture>(request);

            // add to database
            await _pictureRepository.CreateAsync(pictureToCreate);

            return pictureToCreate.Id;
        } 
        
        _mapper.Map(request, picture);

        // update to database
        await _pictureRepository.UpdateAsync(picture);

        return picture.Id;        
    }
}
