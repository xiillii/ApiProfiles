using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetProfileDetails;

public class GetProfileDetailsQueryHandler : IRequestHandler<GetProfileDetailsQuery, ProfileDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IProfileRepository _repository;

    public GetProfileDetailsQueryHandler(IMapper mapper, IProfileRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ProfileDetailsDto> Handle(GetProfileDetailsQuery request, CancellationToken cancellationToken)
    {
        // read the database
        var profile = await _repository.GetByIdAsync(request.Id)
                      ?? throw new NotFoundException(nameof(Domain.Profile), request.Id);

        // convert data objest to dto
        var profileDto = _mapper.Map<ProfileDetailsDto>(profile);

        // return the dto
        return profileDto;
    }
}
