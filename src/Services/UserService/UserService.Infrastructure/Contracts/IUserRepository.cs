using UserService.Domain.Entities;

namespace UserService.Infrastracture.Contracts;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByNicknameAsync(string username);
    Task<User?> GetByRoleAsync(string role);
    Task<IReadOnlyList<User>> GetByStatusAsync(string email);
}