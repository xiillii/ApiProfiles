using ApiPersonProfiles.Infrastructure.Persistence.DatabaseContext;
using ApiPersonProfiles.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace ApiPersonProfiles.Tests.Persistence.IntegrationTests.Repositories;

public class PictureRepositoryTests
{
    private readonly EFDatabaseContext _dbContext;

    public PictureRepositoryTests()
    {
        var dbOptions = new DbContextOptionsBuilder<EFDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new EFDatabaseContext(dbOptions);
    }

    [Fact]
    public async Task CreateSuccessTest()
    {
        // arrange
        var repository = new PictureRepositoryImpl(_dbContext);
        var picture = new Core.Domain.Picture
        {
            FileName = "Filename 1",
            ProfileId = 1,
            ThumbnailFileName = "Filename 1"
        };

        // act
        await repository.CreateAsync(picture);

        // assert
        picture.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async Task UpdateAndGetByIdSuccessTest()
    {
        // arrange
        var repository = new PictureRepositoryImpl(_dbContext);
        var picture = new Core.Domain.Picture
        {
            FileName = "Filename 1",
            ProfileId = 1,
            ThumbnailFileName = "Filename 1"
        };
        var fileNameModified = "File name updated";

        // act
        await repository.CreateAsync(picture);
        picture.FileName = fileNameModified;
        await repository.UpdateAsync(picture);

        var profileModified = await repository.GetByIdAsync(picture.Id);

        // assert
        picture.DateUpdated.ShouldNotBeNull();
        profileModified.ShouldNotBeNull();
        profileModified.FileName.ShouldBe(fileNameModified);

    }

    [Fact]
    public async Task DeleteSuccessTest()
    {
        // arrange
        var repository = new PictureRepositoryImpl(_dbContext);
        var picture = new Core.Domain.Picture
        {
            FileName = "Filename 1",
            ProfileId = 1,
            ThumbnailFileName = "Filename 1"
        };

        // act
        await repository.CreateAsync(picture);
        await repository.DeleteAsync(picture);

        var pictureDeleted = await repository.GetByIdAsync(picture.Id);

        // assert
        picture.DateUpdated.ShouldNotBeNull();
        pictureDeleted.ShouldBeNull();
    }


    [Fact]
    public async Task ListSuccessTest()
    {
        // arrange
        var repository = new PictureRepositoryImpl(_dbContext);
        var picture1 = new Core.Domain.Picture
        {
            FileName = "Filename 1",
            ProfileId = 1,
            ThumbnailFileName = "Filename 1"
        };
        var picture2 = new Core.Domain.Picture
        {
            FileName = "Filename 2",
            ProfileId = 2,
            ThumbnailFileName = "Filename 2"
        };

        // act
        await repository.CreateAsync(picture1);
        await repository.CreateAsync(picture2);


        var profiles = await repository.GetAsync();

        // assert
        profiles.ShouldNotBeNull();
        profiles.Count().ShouldBe(2);
    }

    [Fact]
    public async Task GetByProfileIdTest()
    {
        // arrange
        var repository = new PictureRepositoryImpl(_dbContext);
        var picture1 = new Core.Domain.Picture
        {
            FileName = "Filename 1",
            ProfileId = 1,
            ThumbnailFileName = "Filename 1"
        };
        var picture2 = new Core.Domain.Picture
        {
            FileName = "Filename 2",
            ProfileId = 2,
            ThumbnailFileName = "Filename 2"
        };
        var picture3 = new Core.Domain.Picture
        {
            FileName = "Filename 3",
            ProfileId = 4,
            ThumbnailFileName = "Filename 3"
        };

        // act
        await repository.CreateAsync(picture1);
        await repository.CreateAsync(picture2);
        await repository.CreateAsync(picture3);


        var profile = await repository.GetByProfileIdAsync(4);

        // assert
        profile.ShouldNotBeNull();
        profile.Id.ShouldBe(picture3.Id);
        profile.ProfileId.ShouldBe(4);
    }
}
