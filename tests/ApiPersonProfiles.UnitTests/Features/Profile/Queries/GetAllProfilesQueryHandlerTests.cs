using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetAllProfiles;
using ApiPersonProfiles.Core.Application.MappingProfiles;
using ApiPersonProfiles.Tests.Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace ApiPersonProfiles.Tests.Application.UnitTests.Features.Profile.Queries;

public class GetAllProfilesQueryHandlerTests
{
    private readonly Mock<IProfileRepository> _mockRepository;
    private readonly IMapper _mapper;


    public GetAllProfilesQueryHandlerTests()
    {
        _mockRepository = MockProfileRepository.GetProfileRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<ProfileProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetAllProfilesTest()
    {
        // arrange
        var handler = new GetAllProfilesQueryHandler(_mapper, _mockRepository.Object);

        // act
        var result = await handler.Handle(new GetAllProfilesQuery(), CancellationToken.None);

        // assert
        result.ShouldBeOfType<List<ProfileDto>>();
        result.Count.ShouldBe(2);
    }
}
