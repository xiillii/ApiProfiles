using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Domain;
using ApiPersonProfiles.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonProfiles.Infrastructure.Persistence.Repositories;

// TODO: Implement the file upload to a file repository
public class PictureRepositoryImpl : GenericRepositoryImpl<Picture>, IPictureRepository
{
    public PictureRepositoryImpl(EFDatabaseContext context) : base(context)
    {
    }

    public async Task<Picture> GetByProfileIdAsync(int profileId)
    {
        return await _context.Pictures.FirstOrDefaultAsync(p => p.ProfileId == profileId);
    }
}
