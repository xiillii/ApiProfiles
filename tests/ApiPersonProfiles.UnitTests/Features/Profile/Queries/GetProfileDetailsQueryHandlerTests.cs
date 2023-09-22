using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetProfileDetails;
using ApiPersonProfiles.Core.Application.MappingProfiles;
using ApiPersonProfiles.Tests.Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace ApiPersonProfiles.Tests.Application.UnitTests.Features.Profile.Queries;

public class GetProfileDetailsQueryHandlerTests
{
    private readonly Mock<IProfileRepository> _mockRepository;
    private readonly IMapper _mapper;


    public GetProfileDetailsQueryHandlerTests()
    {
        _mockRepository = MockProfileRepository.GetProfileRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<ProfileProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetProfileDetailsTest()
    {
        // arrange
        var handler = new GetProfileDetailsQueryHandler(_mapper, _mockRepository.Object);

        // act
        var result = await handler.Handle(new GetProfileDetailsQuery(1), CancellationToken.None);

        // assert
        result.ShouldBeOfType<ProfileDetailsDto>();
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetProfileDetailsNotFoundTest()
    {
        // arrange
        var handler = new GetProfileDetailsQueryHandler(_mapper, _mockRepository.Object);

        // act
        var result = await handler.Handle(new GetProfileDetailsQuery(100), CancellationToken.None);

        // assert
        
        result.ShouldBeNull();
    }
}
