using UserService.Domain.Common;
using UserService.Domain.Entities;
namespace UserService.Infrastracture.Contracts;

public interface IRepositoryBase<T> where T : EntityBase
{
    public Task<T> GetByIdAsync(Guid id);
    public Task<T> GetAllAsync();
    public Task<T> AddAsync(T entity);
    public Task<T> UpdateAsync(T entity);
    public Task<T> DeleteAsync(T entity);
}