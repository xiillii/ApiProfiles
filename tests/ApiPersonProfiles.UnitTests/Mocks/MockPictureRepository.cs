using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Domain;
using Moq;

namespace ApiPersonProfiles.Tests.Application.UnitTests.Mocks;

public class MockPictureRepository
{
    public static Mock<IPictureRepository> GetPictureRepository()
    {
        var pictures = new List<Picture>
        {
            new Picture
            {
                Id = 1,
                ProfileId = 2,
                ThumbnailFileName = "/profiles/thumbnail1.png",
                FileName = "/profiles/picture1.png",
            },
            new Picture
            {
                Id = 2,
                ProfileId = 1,
                ThumbnailFileName = "/profiles/thumbnail2.png",
                FileName = "/profiles/picture2.png",
            },
            new Picture
            {
                Id = 3,
                ProfileId = 3,
                ThumbnailFileName = "/profiles/thumbnail3.png",
                FileName = "/profiles/picture3.png",
            },
        };

        var mockRepo = new Mock<IPictureRepository>();

        // Setups
        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(pictures);
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).Returns((int id) =>
        {
            return Task.FromResult(pictures.Find(x => x.Id == id));
        });
        mockRepo.Setup(r => r.GetByProfileIdAsync(It.IsAny<int>())).Returns((int profileId) =>
        {
            return Task.FromResult(pictures.Find(x => x.ProfileId == profileId));   
        });

        return mockRepo;
    }
}
