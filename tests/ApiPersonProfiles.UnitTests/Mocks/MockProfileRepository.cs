using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Domain;
using Moq;

namespace ApiPersonProfiles.Tests.Application.UnitTests.Mocks;

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
        mockRepo.Setup(r => r.IsNicknameUnique(It.IsAny<string>())).Returns((string nickname) =>
        {
            var item = profiles.Find(x => nickname.Equals(x.Nickname, StringComparison.InvariantCultureIgnoreCase));

            return Task.FromResult(item == null);
        });
        mockRepo.Setup(r => r.IsNicknameUnique(It.IsAny<string>(), It.IsAny<int>())).Returns((string nickname, int id) =>
        {
            var item = profiles.Find(x => x.Id != id && x.Nickname.Equals(nickname, StringComparison.InvariantCultureIgnoreCase));

            return Task.FromResult(item == null);
        });
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).Returns((int id) =>
        {
            return Task.FromResult(profiles.Find(x => x.Id == id));
        });
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<Profile>())).Returns((Profile profile) =>
        {
            profiles.Add(profile);

            return Task.FromResult(profile);
        });
        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Profile>())).Returns((Profile profile) =>
        {
            // get the item
            var item = profiles.Find(p => p.Id == profile.Id);

            if (item != null)
            {
                // update
                item.FirstName = profile.FirstName;
                item.LastName = profile.LastName;
                item.Age = profile.Age;
                item.Nickname = profile.Nickname;
            }


            // return nothing
            return Task.FromResult(item);
        });
        mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Profile>())).Returns((Profile profile) =>
        {
            // get the item
            var item = profiles.Find(p => p.Id == profile.Id);

            if (item != null)
            {
                // delete
               profiles.Remove(item);
            }


            // return nothing
            return Task.FromResult(item);
        });

        return mockRepo;
    }
}
