using ApiPersonProfiles.Core.Application.Contracts.Persistence;
using ApiPersonProfiles.Core.Domain.Common;
using ApiPersonProfiles.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonProfiles.Infrastructure.Persistence.Repositories;

public class GenericRepositoryImpl<T> : IGenericRepository<T> where T : EntityBase
{
    protected readonly EFDatabaseContext _context;

    public GenericRepositoryImpl(EFDatabaseContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync()
        => await _context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(int id)
    {
        var res =  await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

        return res;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
