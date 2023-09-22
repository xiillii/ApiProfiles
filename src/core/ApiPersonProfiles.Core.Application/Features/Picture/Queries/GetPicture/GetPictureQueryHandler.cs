using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace ApiPersonProfiles.Core.Application.Features.Picture.Queries.GetPicture;

public class GetPictureQueryHandler : IRequestHandler<GetPictureQuery, PictureDto>
{
    private readonly IMapper _mapper;
    private readonly IPictureRepository _repository;

    public GetPictureQueryHandler(IMapper mapper, IPictureRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PictureDto> Handle(GetPictureQuery request, CancellationToken cancellationToken)
    {
        // read the database
        var picture = await _repository.GetByProfileIdAsync(request.ProfileId)
            ?? throw new NotFoundException(nameof(Domain.Picture), request.ProfileId);

        // convert data to dto
        var pictureDto = _mapper.Map<PictureDto>(picture);

        // return the dto
        return pictureDto;
    }
}
