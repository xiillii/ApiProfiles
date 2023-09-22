using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Commands.DeleteProfile;

public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IProfileRepository _repository;

    public DeleteProfileCommandHandler(IMapper mapper, IProfileRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        // get the data from database
        var profileToDelete = await _repository.GetByIdAsync(request.Id)
                              ?? throw new NotFoundException(nameof(Domain.Profile), request.Id);

        // delete from database
        await _repository.DeleteAsync(profileToDelete);

        // return nothing
        return Unit.Value;
    }
}
