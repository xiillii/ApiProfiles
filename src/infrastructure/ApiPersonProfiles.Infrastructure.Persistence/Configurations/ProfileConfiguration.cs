using ApiPersonProfiles.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPersonProfiles.Infrastructure.Persistence.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasData(new Profile
        {
            Id = 1,
            FirstName = "James",
            LastName = "Bond",
            Age = 24,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now,
        });

        builder.Property(q => q.Nickname)
            .IsRequired()
            .HasMaxLength(20);
    }
}
