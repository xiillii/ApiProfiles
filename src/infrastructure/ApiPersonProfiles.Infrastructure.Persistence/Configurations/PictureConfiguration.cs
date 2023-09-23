using ApiPersonProfiles.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPersonProfiles.Infrastructure.Persistence.Configurations;

public class PictureConfiguration : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.HasData(new Picture
        {
            Id = 1,
            ProfileId = 1,
            FileName = $"{nameof(Picture)}.jpg",
            ThumbnailFileName = $"{nameof(Picture)}.jpg",
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now,
        });
    }
}
