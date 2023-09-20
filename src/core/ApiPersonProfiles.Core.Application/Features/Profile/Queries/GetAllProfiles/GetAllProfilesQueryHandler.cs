using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetAllProfiles;

public class GetAllProfilesQueryHandler : IRequestHandler<GetAllProfilesQuery, List<ProfileDto>>
{
    private readonly IMapper _mapper;
    private readonly IProfileRepository _repository;

    public GetAllProfilesQueryHandler(IMapper mapper, IProfileRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<ProfileDto>> Handle(GetAllProfilesQuery request
        , CancellationToken cancellationToken)
    {
        // Read the database
        var profiles = await _repository.GetAsync();

        // Convert data objects to Dto
        var profilesList = _mapper.Map<List<ProfileDto>>(profiles);

        // Return the list
        return profilesList;
    }
}
