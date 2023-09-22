using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Commands.CreateProfile;

public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IProfileRepository _repository;

    public CreateProfileCommandHandler(IMapper mapper, IProfileRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<int> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateProfileCommandValidator(_repository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Profile", validationResult);
        }

        // Convert DTO to domain entity
        var profileToCreate = _mapper.Map<Domain.Profile>(request);

        // add to database
        await _repository.CreateAsync(profileToCreate);

        // return id
        return profileToCreate.Id;
    }
}
