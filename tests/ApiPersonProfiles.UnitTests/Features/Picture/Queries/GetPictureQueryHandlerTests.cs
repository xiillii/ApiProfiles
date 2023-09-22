using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Exceptions;
using ApiPersonProfiles.Core.Application.Features.Picture.Queries.GetPicture;
using ApiPersonProfiles.Core.Application.MappingProfiles;
using ApiPersonProfiles.Tests.Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace ApiPersonProfiles.Tests.Application.UnitTests.Features.Picture.Queries;

public class GetPictureQueryHandlerTests
{
    private readonly Mock<IPictureRepository> _mockRepository;
    private readonly IMapper _mapper;


    public GetPictureQueryHandlerTests()
    {
        _mockRepository = MockPictureRepository.GetPictureRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<PictureProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetPictureSuccessTest()
    {
        // arrage
        var handler = new GetPictureQueryHandler(_mapper, _mockRepository.Object);

        // act
        var result = await handler.Handle(new GetPictureQuery(3), CancellationToken.None);

        // assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<PictureDto>();
        result.Id.ShouldBe(3);
    }

    [Fact]
    public void GetPictureNotFoundTest()
    {
        // arrage
        var handler = new GetPictureQueryHandler(_mapper, _mockRepository.Object);

        // act and asser
        Should.Throw<NotFoundException>(async () => await handler.Handle(new GetPictureQuery(300), CancellationToken.None));

        
    }
}
