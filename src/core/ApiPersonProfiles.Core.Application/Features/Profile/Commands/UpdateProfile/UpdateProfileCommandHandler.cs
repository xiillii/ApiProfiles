using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IProfileRepository _repository;

    public UpdateProfileCommandHandler(IMapper mapper, IProfileRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        // get the item
        var profileToUpdate = await _repository.GetByIdAsync(request.Id)
                              ?? throw new NotFoundException(nameof(Domain.Profile), request.Id);

        // Validate incomming data
        var validator = new UpdateProfileCommandValidator(_repository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Profile", validationResult);
        }

        // convert to domain entity object
        // this update and convert to entity object
        _mapper.Map(request, profileToUpdate);

        // add to database
        await _repository.UpdateAsync(profileToUpdate);

        // return nothing
        return Unit.Value;
    }
}
