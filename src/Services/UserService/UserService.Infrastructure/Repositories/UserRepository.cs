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
    public async Task<User?> GetByNicknameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken: cancellationToken);
    }
    public async Task<IReadOnlyList<User>> GetByRoleAsync(string role,CancellationToken cancellationToken = default)
    {
        return await _context.Users.Where(u => u.Role == role).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<User>> GetByStatusAsync(UserStatus status,CancellationToken cancellationToken = default)
    {
        return await _context.Users.Where(u => u.Status == status).ToListAsync(cancellationToken: cancellationToken);
    }
    
}