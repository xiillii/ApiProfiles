using ApiPersonProfiles.Infrastructure.Persistence.DatabaseContext;
using ApiPersonProfiles.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace ApiPersonProfiles.Tests.Persistence.IntegrationTests.Repositories;

public class ProfileRepositoryTests
{
    private readonly EFDatabaseContext _dbContext;

    public ProfileRepositoryTests()
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
        var repository = new ProfileRepositoryImpl(_dbContext);
        var profile = new Core.Domain.Profile
        {
            FirstName = "First Name",
            LastName = "Last Name",
            Age = 1,
            Nickname = "pepeloncho"
        };

        // act
        await repository.CreateAsync(profile);

        // assert
        profile.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async Task UpdateAndGetByIdSuccessTest()
    {
        // arrange
        var repository = new ProfileRepositoryImpl(_dbContext);
        var profile = new Core.Domain.Profile
        {
            FirstName = "First Name",
            LastName = "Last Name",
            Age = 1,
            Nickname = "pepeloncho"
        };
        var nameModified = "First Name Update";

        // act
        await repository.CreateAsync(profile);
        profile.FirstName = nameModified;
        await repository.UpdateAsync(profile);

        var profileModified = await repository.GetByIdAsync(profile.Id);

        // assert
        profile.DateUpdated.ShouldNotBeNull();
        profileModified.ShouldNotBeNull();
        profileModified.FirstName.ShouldBe(nameModified);
        
    }

    [Fact]
    public async Task DeleteSuccessTest()
    {
        // arrange
        var repository = new ProfileRepositoryImpl(_dbContext);
        var profile = new Core.Domain.Profile
        {
            FirstName = "First Name",
            LastName = "Last Name",
            Age = 1,
            Nickname = "pepeloncho"
        };        

        // act
        await repository.CreateAsync(profile);        
        await repository.DeleteAsync(profile);

        var profileDeleted = await repository.GetByIdAsync(profile.Id);

        // assert
        profile.DateUpdated.ShouldNotBeNull();
        profileDeleted.ShouldBeNull();        
    }

    [Fact]
    public async Task ListSuccessTest()
    {
        // arrange
        var repository = new ProfileRepositoryImpl(_dbContext);
        var profile1 = new Core.Domain.Profile
        {
            FirstName = "First Name",
            LastName = "Last Name",
            Age = 1,
            Nickname = "pepeloncho"
        };
        var profile2 = new Core.Domain.Profile
        {
            FirstName = "First Name",
            LastName = "Last Name",
            Age = 1,
            Nickname = "pepeloncho"
        };

        // act
        await repository.CreateAsync(profile1);
        await repository.CreateAsync(profile2);
        

        var profiles = await repository.GetAsync();

        // assert
        profiles.ShouldNotBeNull();
        profiles.Count().ShouldBe(2);
    }

    [Fact]
    public async Task IsNicknameUniqueTrueTest()
    {
        // arrange
        var repository = new ProfileRepositoryImpl(_dbContext);
        var profile1 = new Core.Domain.Profile
        {
            FirstName = "First Name",
            LastName = "Last Name",
            Age = 1,
            Nickname = "pepeloncho"
        };
        var profile2 = new Core.Domain.Profile
        {
            FirstName = "First Name2",
            LastName = "Last Name2",
            Age = 1,
            Nickname = "pepeloncho2"
        };

        // act
        await repository.CreateAsync(profile1);
        await repository.CreateAsync(profile2);

        var isUnique = await repository.IsNicknameUnique("tommy");

        // assert        
        isUnique.ShouldBeTrue();
    }

    [Fact]
    public async Task IsNicknameUniqueWithIdTrueTest()
    {
        // arrange
        var repository = new ProfileRepositoryImpl(_dbContext);
        var profile1 = new Core.Domain.Profile
        {
            FirstName = "First Name",
            LastName = "Last Name",
            Age = 1,
            Nickname = "pepeloncho"
        };
        var profile2 = new Core.Domain.Profile
        {
            FirstName = "First Name2",
            LastName = "Last Name2",
            Age = 1,
            Nickname = "pepeloncho2"
        };

        // act
        await repository.CreateAsync(profile1);
        await repository.CreateAsync(profile2);

        var isUnique = await repository.IsNicknameUnique("pepeloncho", 1);

        // assert        
        isUnique.ShouldBeTrue();
    }

    [Fact]
    public async Task IsNicknameUniqueFalseTest()
    {
        // arrange
        var repository = new ProfileRepositoryImpl(_dbContext);
        var profile1 = new Core.Domain.Profile
        {
            FirstName = "First Name",
            LastName = "Last Name",
            Age = 1,
            Nickname = "pepeloncho"
        };
        var profile2 = new Core.Domain.Profile
        {
            FirstName = "First Name2",
            LastName = "Last Name2",
            Age = 1,
            Nickname = "pepeloncho2"
        };

        // act
        await repository.CreateAsync(profile1);
        await repository.CreateAsync(profile2);

        var isUnique = await repository.IsNicknameUnique("pepeloncho");

        // assert        
        isUnique.ShouldBeFalse();
    }

    [Fact]
    public async Task IsNicknameUniqueWithIdFalseTest()
    {
        // arrange
        var repository = new ProfileRepositoryImpl(_dbContext);
        var profile1 = new Core.Domain.Profile
        {
            FirstName = "First Name",
            LastName = "Last Name",
            Age = 1,
            Nickname = "pepeloncho"
        };
        var profile2 = new Core.Domain.Profile
        {
            FirstName = "First Name2",
            LastName = "Last Name2",
            Age = 1,
            Nickname = "pepeloncho2"
        };

        // act
        await repository.CreateAsync(profile1);
        await repository.CreateAsync(profile2);

        var isUnique = await repository.IsNicknameUnique("pepeloncho2", 1);

        // assert        
        isUnique.ShouldBeFalse();
    }
}
