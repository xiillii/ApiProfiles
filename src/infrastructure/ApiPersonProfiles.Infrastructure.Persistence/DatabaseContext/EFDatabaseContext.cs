using ApiPersonProfiles.Core.Domain;
using ApiPersonProfiles.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonProfiles.Infrastructure.Persistence.DatabaseContext;

public class EFDatabaseContext : DbContext
{
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Picture> Pictures { get; set; }

    public EFDatabaseContext(DbContextOptions<EFDatabaseContext> options) : base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDatabaseContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<EntityBase>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified)) 
        {
            entry.Entity.DateUpdated = DateTime.UtcNow;

            if (entry.State is EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
