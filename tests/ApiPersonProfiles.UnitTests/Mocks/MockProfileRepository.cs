using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Domain;
using Moq;

namespace ApiPersonProfiles.Application.UnitTests.Mocks;

public class MockProfileRepository
{
    public static Mock<IProfileRepository> GetProfileRepository()
    {
        var profile = new Profile
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Age = 10,
            Nickname = "fulanito"
        };

        var profiles = new List<Profile>
        {
            new Profile
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Age = 10,
                Nickname = "fulanito"
            },
            new Profile
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Age = 20,
                Nickname = "fulanita"
            },
        };

        var mockRepo = new Mock<IProfileRepository>();

        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(profiles);
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(profile);
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<Profile>())).Returns((Profile profile) =>
        {
            profiles.Add(profile);

            return Task.FromResult(profile);
        });

        return mockRepo;
    }
}
