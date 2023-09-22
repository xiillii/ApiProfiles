using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Exceptions;
using ApiPersonProfiles.Core.Application.Features.Picture.Commands.RemovePicture;
using ApiPersonProfiles.Core.Application.Features.Picture.Queries.GetPicture;
using ApiPersonProfiles.Core.Application.MappingProfiles;
using ApiPersonProfiles.Tests.Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace ApiPersonProfiles.Tests.Application.UnitTests.Features.Picture.Commands;

public class RemovePictureCommandHandlerTests
{
    private readonly Mock<IProfileRepository> _mockProfileRepository;
    private readonly Mock<IPictureRepository> _mockPictureRepository;
    private readonly IMapper _mapper;


    public RemovePictureCommandHandlerTests()
    {
        _mockProfileRepository = MockProfileRepository.GetProfileRepository();
        _mockPictureRepository = MockPictureRepository.GetPictureRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<PictureProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task RemovePictureWithPreviousDataSuccessTest()
    {
        // arrange
        var handlerGet = new GetPictureQueryHandler(_mapper, _mockPictureRepository.Object);    
        var handler = new RemovePictureCommandHandler(_mapper
            , _mockProfileRepository.Object
            , _mockPictureRepository.Object);
        var command = new RemovePictureCommand
        {        
            ProfileId = 2
        };

        // act
        await handler.Handle(command, CancellationToken.None);

        // assert
        Should.Throw<NotFoundException>(async () => await handlerGet.Handle(new GetPictureQuery(2), CancellationToken.None));                
    }

    [Fact]
    public async Task RemovePictureWithoutPreviousDataSuccessTest()
    {
        // arrange
        var handlerGet = new GetPictureQueryHandler(_mapper, _mockPictureRepository.Object);
        var handler = new RemovePictureCommandHandler(_mapper
            , _mockProfileRepository.Object
            , _mockPictureRepository.Object);
        var command = new RemovePictureCommand
        {
            ProfileId = 4
        };

        // act
        await handler.Handle(command, CancellationToken.None);

        // assert
        Should.Throw<NotFoundException>(async () => await handlerGet.Handle(new GetPictureQuery(4), CancellationToken.None));
    }

    [Fact]
    public void RemovePictureProfileNotFoundTest()
    {
        // arrange        
        var handler = new RemovePictureCommandHandler(_mapper
            , _mockProfileRepository.Object
            , _mockPictureRepository.Object);
        var command = new RemovePictureCommand
        {
            ProfileId = 100
        };

        // act
        Should.Throw<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }
}
