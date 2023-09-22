using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Application.Features.Profile.Commands.DeleteProfile;
using ApiPersonProfiles.Core.Application.Features.Profile.Commands.UpdateProfile;
using ApiPersonProfiles.Core.Application.Features.Profile.Queries.GetProfileDetails;
using ApiPersonProfiles.Core.Application.MappingProfiles;
using ApiPersonProfiles.Tests.Application.UnitTests.Mocks;
using AutoMapper;
using MediatR;
using Moq;
using Shouldly;

namespace ApiPersonProfiles.Tests.Application.UnitTests.Features.Profile.Commands;

public class DeleteProfileCommandHandlerTests
{
    private readonly Mock<IProfileRepository> _mockRepository;
    private readonly IMapper _mapper;


    public DeleteProfileCommandHandlerTests()
    {
        _mockRepository = MockProfileRepository.GetProfileRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<ProfileProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task DeleteProfileSuccessTest()
    {
        // arrange
        var getHandler = new GetProfileDetailsQueryHandler(_mapper, _mockRepository.Object);
        var handler = new DeleteProfileCommandHandler(_mapper, _mockRepository.Object);
        var command = new DeleteProfileCommand
        {
            Id = 1
        };

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.ShouldBeOfType<Unit>();

        var item = await getHandler.Handle(new GetProfileDetailsQuery(1), CancellationToken.None);

        item.ShouldBeNull();
        
    }
}
