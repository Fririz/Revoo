using UserService.Domain.Common;
using UserService.Domain.Entities;
namespace UserService.Application.Contracts;

public interface IRepositoryBase<T> where T : EntityBase
{
    public Task<T?> GetByIdAsync(Guid id);
    public Task<IReadOnlyList<T?>> GetAllAsync();
    public Task<T> AddAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(T entity);
}