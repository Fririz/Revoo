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
    //Constructor for other cached repo bcs they have another context
    protected CachedRepositoryBase(IDistributedCache cache, ISerializer serializer)
    {
        _cache = cache;
        _serializer = serializer;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        string? cached = await _cache.GetStringAsync(id.ToString(), token: cancellationToken);
        if (!string.IsNullOrEmpty(cached))
        {
            return _serializer.Deserialize<T>(cached);
        }
        T? value = await _context.GetByIdAsync(id, cancellationToken);
        
        if (value == null) return null;
        await _cache.SetStringAsync(id.ToString(), _serializer.Serialize(value), token: cancellationToken);
        return value;
    }

    public async Task<IReadOnlyList<T?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.GetAllAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity,CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(entity, cancellationToken);
        await _cache.SetStringAsync(entity.Id.ToString(), _serializer.Serialize(entity), token: cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(T entity,CancellationToken cancellationToken = default)
    {
        await _context.UpdateAsync(entity, cancellationToken);
        await _cache.RemoveAsync(entity.Id.ToString(), cancellationToken);
    }

    public async Task DeleteAsync(T entity,CancellationToken cancellationToken = default)
    {
        await _context.DeleteAsync(entity, cancellationToken);
        await _cache.RemoveAsync(entity.Id.ToString(), cancellationToken);
    }
}