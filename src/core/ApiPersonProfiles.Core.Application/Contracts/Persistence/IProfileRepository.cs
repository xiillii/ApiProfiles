using ApiPersonProfiles.Core.Domain;

namespace ApiPersonProfiles.Core.Application.Contracts.Persistence;

public interface IProfileRepository : IGenericRepository<Profile>
{
    Task<bool> IsNicknameUnique(string nickName);
    /// <summary>
    /// Verify in each item where Identifier is not "id"
    /// </summary>
    /// <param name="nickname"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> IsNicknameUnique(string? nickname, int id);
}
