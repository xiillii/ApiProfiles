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
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<Picture>())).Returns((Picture picture) =>
        {
            picture.Id = pictures.Max(x => x.Id) + 1;
            pictures.Add(picture);

            return Task.FromResult(picture);
        });
        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Picture>())).Returns((Picture picture) =>
        {
            // get the item
            var item = pictures.Find(p => p.Id == picture.Id);

            if (item != null)
            {
                // update
                item.ProfileId = picture.ProfileId;
                item.ThumbnailFileName = picture.ThumbnailFileName;
                item.FileName = picture.FileName;
            }

            return Task.FromResult(item);
        });

        return mockRepo;
    }
}
