using UserService.Domain.Common;
using UserService.Infrastracture.Contracts;
using UserService.Infrastracture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastracture.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
{
    protected UserContext _context;
    public RepositoryBase(UserContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T?>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    public async Task<T> AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}