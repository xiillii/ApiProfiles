using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Exceptions;
using ApiPersonProfiles.Core.Application.Features.Picture.Commands.UploadPicture;
using ApiPersonProfiles.Core.Application.MappingProfiles;
using ApiPersonProfiles.Tests.Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace ApiPersonProfiles.Tests.Application.UnitTests.Features.Picture.Commands;

public class UploadPictureCommandHandlerTests
{
    private readonly Mock<IProfileRepository> _mockProfileRepository;
    private readonly Mock<IPictureRepository> _mockPictureRepository;
    private readonly IMapper _mapper;


    public UploadPictureCommandHandlerTests()
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
    public async Task UploadPictureWithoutPreviousDataSuccessTest()
    {
        // arrange
        var handler = new UploadPictureCommandHandler(_mapper
            , _mockProfileRepository.Object
            , _mockPictureRepository.Object);
        var command = new UploadPictureCommand
        {
            FileName = "path1/path2/picture2.png",
            ThumbnailFileName = "path1/path2/picturethumb2.png",
            ProfileId = 4
        };

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.ShouldBeOfType<int>();
        result.ShouldBe(4);
    }

    [Fact]
    public async Task UploadPictureWitPreviousDataSuccessTest()
    {
        // arrange
        var handler = new UploadPictureCommandHandler(_mapper
            , _mockProfileRepository.Object
            , _mockPictureRepository.Object);
        var command = new UploadPictureCommand
        {
            FileName = "path1/path2/picture2.png",
            ThumbnailFileName = "path1/path2/picturethumb2.png",
            ProfileId = 2
        };

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.ShouldBeOfType<int>();
        result.ShouldBe(1);
    }

    [Fact]
    public void UploadPictureBadRequestTest()
    {
        // arrange
        var handler = new UploadPictureCommandHandler(_mapper
            , _mockProfileRepository.Object
            , _mockPictureRepository.Object);
        var command = new UploadPictureCommand
        {
            FileName = null,
            ThumbnailFileName = "path1/path2/picturethumb2.png",
            ProfileId = 2
        };

        // act and assert
        Should.Throw<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public void UploadPictureNotFoundTest()
    {
        // arrange
        var handler = new UploadPictureCommandHandler(_mapper
            , _mockProfileRepository.Object
            , _mockPictureRepository.Object);
        var command = new UploadPictureCommand
        {
            FileName = null,
            ThumbnailFileName = "path1/path2/picturethumb2.png",
            ProfileId = 100
        };

        // act and assert
        Should.Throw<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }
}
