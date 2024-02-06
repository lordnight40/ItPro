using ItPro.Core.Repository.Queries;
using ItPro.Data.Entities;

namespace ItPro.Core.Repository;

public interface IRepository<T> where T: BaseEntity
{
    Task<PagedObject<T>> GetAllAsync(QueryStringParameters queryString, CancellationToken cancellationToken = default);

    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}