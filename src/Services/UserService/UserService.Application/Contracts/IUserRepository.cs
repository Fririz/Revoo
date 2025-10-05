using UserService.Domain.Entities;

namespace UserService.Application.Contracts;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByNicknameAsync(string username);
    Task<IReadOnlyList<User>> GetByRoleAsync(string role);
    Task<IReadOnlyList<User>> GetByStatusAsync(Domain.Enums.UserStatus status);
}