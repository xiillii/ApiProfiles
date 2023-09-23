using ApiPersonProfiles.Core.Domain;
using ApiPersonProfiles.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace ApiPersonProfiles.Tests.Persistence.IntegrationTests;

public class DatabaseContextTests
{
    private readonly EFDatabaseContext _dbContext;

    public DatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<EFDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new EFDatabaseContext(dbOptions);
    }

    [Fact]
    public async Task ProfileSave_SetDateCreatedValue()
    {
        // arrange
        var profile = new Profile
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            Age = 1,
            Nickname = "Test",
        };

        // act
        await _dbContext.Profiles.AddAsync(profile);
        await _dbContext.SaveChangesAsync();

        // assert
        profile.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async Task ProfileSave_SetDateUpdatedValue()
    {
        // arrange
        var profile = new Profile
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            Age = 1,
            Nickname = "Test",
        };

        // act
        await _dbContext.Profiles.AddAsync(profile);
        await _dbContext.SaveChangesAsync();

        // assert
        profile.DateUpdated.ShouldNotBeNull();
    }

    [Fact]
    public async Task ProfileUpdate_SetDateUpdatedValue()
    {
        // arrange
        var profile = new Profile
        {
            Id = 1,
            FirstName = "Test Updated",
            LastName = "Test",
            Age = 1,
            Nickname = "Test",
        };

        // act
        await _dbContext.Profiles.AddAsync(profile);
        await _dbContext.SaveChangesAsync();
        var dateUpdated = profile.DateUpdated;

        _dbContext.Profiles.Update(profile);
        await _dbContext.SaveChangesAsync();

        // assert
        profile.DateUpdated.ShouldNotBeNull();
        dateUpdated.ShouldNotBe(profile.DateUpdated);
    }

    [Fact]
    public async Task PictureSave_SetDateCreatedValue()
    {
        // arrange
        var picture = new Picture
        {
            Id = 1,
            FileName = "Test",
            ThumbnailFileName = "Test",
            ProfileId = 1,
        };

        // act
        await _dbContext.Pictures.AddAsync(picture);
        await _dbContext.SaveChangesAsync();

        // assert
        picture.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async Task PictureSave_SetDateUpdatedValue()
    {
        // arrange
        var picture = new Picture
        {
            Id = 1,
            FileName = "Test",
            ThumbnailFileName = "Test",
            ProfileId = 1,
        };

        // act
        await _dbContext.Pictures.AddAsync(picture);
        await _dbContext.SaveChangesAsync();

        // assert
        picture.DateUpdated.ShouldNotBeNull();
    }
}
