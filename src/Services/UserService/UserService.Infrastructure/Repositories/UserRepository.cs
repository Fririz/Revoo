using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Enums;
using UserService.Application.Contracts;

namespace UserService.Infrastracture.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(Persistence.UserContext context) : base(context)
    {
        
    }
    public async Task<User?> GetByNicknameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
    public async Task<IReadOnlyList<User>> GetByRoleAsync(string role)
    {
        return await _context.Users.Where(u => u.Role == role).ToListAsync();
    }

    public async Task<IReadOnlyList<User>> GetByStatusAsync(UserStatus status)
    {
        return await _context.Users.Where(u => u.Status == status).ToListAsync();
    }
    
}