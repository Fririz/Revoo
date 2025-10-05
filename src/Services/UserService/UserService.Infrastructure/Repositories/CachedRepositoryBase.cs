using Microsoft.Extensions.Caching.Distributed;
using UserService.Domain.Common;
using UserService.Domain.Entities;
using UserService.Application.Contracts;
using UserService.Infrastracture.Serialization;


namespace UserService.Infrastracture.Repositories;

public class CachedRepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
{
    protected readonly IDistributedCache _cache;
    private readonly IRepositoryBase<T> _context;
    protected readonly ISerializer _serializer;

    public CachedRepositoryBase(IDistributedCache cache, IRepositoryBase<T> context, ISerializer serializer)
    {
        _cache = cache;
        _context = context;
        _serializer = serializer;
    }

    protected CachedRepositoryBase(IDistributedCache cache, ISerializer serializer)
    {
        _cache = cache;
        _serializer = serializer;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        string? cached = await _cache.GetStringAsync(id.ToString());
        if (!string.IsNullOrEmpty(cached))
        {
            return _serializer.Deserialize<T>(cached);
        }
        T? value = await _context.GetByIdAsync(id);
        
        if (value == null) return null;
        await _cache.SetStringAsync(id.ToString(), _serializer.Serialize(value));
        return value;
    }

    public async Task<IReadOnlyList<T?>> GetAllAsync()
    {
        return await _context.GetAllAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _cache.SetStringAsync(entity.Id.ToString(), _serializer.Serialize(entity));
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        await _context.DeleteAsync(entity);
        await _cache.RemoveAsync(entity.Id.ToString());
        await AddAsync(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        await _context.DeleteAsync(entity);
        await _cache.RemoveAsync(entity.Id.ToString());
    }
}