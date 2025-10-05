using Microsoft.Extensions.Caching.Distributed;
using UserService.Domain.Entities;
using UserService.Domain.Enums;
using UserService.Application.Contracts;
using UserService.Infrastracture.Serialization;

namespace UserService.Infrastracture.Repositories;

public class CachedUserRepository : CachedRepositoryBase<User>, IUserRepository
{
    private readonly IUserRepository _context;
    public CachedUserRepository(IUserRepository context, IDistributedCache cache, ISerializer serializer) : base(cache, serializer)
    {
        _context = context;
    }


    public async Task<User?> GetByNicknameAsync(string username)
    {
        string? cachedUser = await _cache.GetStringAsync(username);
        if (!string.IsNullOrEmpty(cachedUser))
        {
            return _serializer.Deserialize<User>(cachedUser);
        }
        User? user = await _context.GetByNicknameAsync(username);
        if (user == null) return null;
        await _cache.SetStringAsync(username, _serializer.Serialize(user));
        return user;
    }

    public async Task<IReadOnlyList<User>> GetByRoleAsync(string role)
    {
       return await _context.GetByRoleAsync(role);
    }

    public async Task<IReadOnlyList<User>> GetByStatusAsync(UserStatus status)
    {
        return await _context.GetByStatusAsync(status);
    }
}