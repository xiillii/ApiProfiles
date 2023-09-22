using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Exceptions;
using ApiPersonProfiles.Core.Application.Features.Profile.Commands.UpdateProfile;
using ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetProfileDetails;
using ApiPersonProfiles.Core.Application.MappingProfiles;
using ApiPersonProfiles.Tests.Application.UnitTests.Mocks;
using AutoMapper;
using MediatR;
using Moq;
using Shouldly;

namespace ApiPersonProfiles.Tests.Application.UnitTests.Features.Profile.Commands;

public class UpdateProfileCommandHandlerTests
{
    private readonly Mock<IProfileRepository> _mockRepository;
    private readonly IMapper _mapper;


    public UpdateProfileCommandHandlerTests()
    {
        _mockRepository = MockProfileRepository.GetProfileRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<ProfileProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task UpdateProfileSuccessTest()
    {
        // arrange
        var getHandler = new GetProfileDetailsQueryHandler(_mapper, _mockRepository.Object);
        var handler = new UpdateProfileCommandHandler(_mapper, _mockRepository.Object);
        var command = new UpdateProfileCommand
        {
            Id = 1,
            FirstName = "first name Updated",
            LastName = "last name Updated",
            Age = 20,
            Nickname = "fulanito"
        };

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.ShouldBeOfType<Unit>();
        
        var item = await getHandler.Handle(new GetProfileDetailsQuery(1), CancellationToken.None);
        
        item.ShouldNotBeNull();
        item.FirstName.ShouldBeSameAs(item.FirstName);
    }

    [Fact]
    public void UpdateProfileBadRequestTest()
    {
        // arrange
        var handler = new UpdateProfileCommandHandler(_mapper, _mockRepository.Object);
        var command = new UpdateProfileCommand
        {
            Id = 1,
            FirstName = null,
            LastName = "last name",
        };

        // assert
        Should.Throw<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public void UpdateProfileNotFoundTest()
    {
        // arrange
        var handler = new UpdateProfileCommandHandler(_mapper, _mockRepository.Object);
        var command = new UpdateProfileCommand
        {
            Id = 500,
            FirstName = "first name updated",
            LastName = "last name updated",
            Age = 40,
            Nickname = "Updated"
        };

      
        // assert
        Should.Throw<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }
}
