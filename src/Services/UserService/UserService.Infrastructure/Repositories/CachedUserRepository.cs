using Microsoft.Extensions.Caching.Distributed;
using UserService.Domain.Entities;
using UserService.Domain.Enums;
using UserService.Infrastracture.Contracts;
namespace UserService.Infrastracture.Repositories;

public class CachedUserRepository : Contracts.IUserRepository
{
    private readonly IUserRepository _context;
    private readonly IDistributedCache _cache;
    public CachedUserRepository(IUserRepository context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<User?>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByNicknameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<User>> GetByRoleAsync(string role)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<User>> GetByStatusAsync(UserStatus status)
    {
        throw new NotImplementedException();
    }
}