using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Features.Profile.Commands.CreateProfile;
using ApiPersonProfiles.Core.Application.MappingProfiles;
using ApiPersonProfiles.Tests.Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace ApiPersonProfiles.Tests.Application.UnitTests.Features.Profile.Commands;

public class CreateProfileCommandHandlerTests
{
    private readonly Mock<IProfileRepository> _mockRepository;
    private readonly IMapper _mapper;


    public CreateProfileCommandHandlerTests()
    {
        _mockRepository = MockProfileRepository.GetProfileRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<ProfileProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CreateProfileSuccessTest()
    {
        // arrange
        var handler = new CreateProfileCommandHandler(_mapper, _mockRepository.Object);
        var command = new CreateProfileCommand
        {
            FirstName = "first name",
            LastName = "last name",
            Age = 20,
        };

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.ShouldBeOfType<int>();
        result.ShouldBeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public async Task CreateProfileBadRequestTest()
    {
        // arrange
        var handler = new CreateProfileCommandHandler(_mapper, _mockRepository.Object);
        var command = new CreateProfileCommand
        {
            FirstName = null,
            LastName = "last name",
        };


        // TODO: Catch Badrequest exception
        // assert
        Should.Throw<Exception>(async () => await handler.Handle(command, CancellationToken.None));
    }
}
