using UserService.Domain.Entities;

namespace UserService.Application.Contracts;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByNicknameAsync(string username,CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetByRoleAsync(string role,CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetByStatusAsync(Domain.Enums.UserStatus status,CancellationToken cancellationToken = default);
}