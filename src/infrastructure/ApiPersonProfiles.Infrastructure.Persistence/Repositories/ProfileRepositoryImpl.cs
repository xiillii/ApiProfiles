using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Domain;
using ApiPersonProfiles.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonProfiles.Infrastructure.Persistence.Repositories;

public class ProfileRepositoryImpl : GenericRepositoryImpl<Profile>, IProfileRepository
{
    public ProfileRepositoryImpl(EFDatabaseContext context) : base(context)
    {
    }

    public async Task<bool> IsNicknameUnique(string nickName)
    {
        var notUnique = await _context.Profiles.AnyAsync(p => p.Nickname.Contains(
            nickName,
            StringComparison.InvariantCultureIgnoreCase));

        return !notUnique;
    }

    public async Task<bool> IsNicknameUnique(string? nickname, int id)
    {
        var notUnique = await _context.Profiles.AnyAsync(p => p.Nickname.Contains(
            nickname,
            StringComparison.InvariantCultureIgnoreCase) && p.Id != id);

        return !notUnique;
    }
}
