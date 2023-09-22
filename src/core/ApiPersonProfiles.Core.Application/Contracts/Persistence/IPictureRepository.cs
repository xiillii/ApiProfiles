using ApiPersonProfiles.Core.Domain;

namespace ApiPersonProfiles.Core.Application.Contracts.Persistence;

public interface IPictureRepository : IGenericRepository<Picture>
{
    Task<Picture> GetByProfileIdAsync(int profileId);
}
