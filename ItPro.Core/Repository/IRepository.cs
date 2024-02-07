using ItPro.Core.Helpful;
using ItPro.Core.Repository.Queries;
using ItPro.Data.Entities;

namespace ItPro.Core.Repository;

/// <summary>
/// Репозиторий с базовыми CRUD операциями над сущностями.
/// </summary>
/// <typeparam name="T">Тип сущности, унаследованный от <see cref="BaseEntity"/></typeparam>
public interface IRepository<T> where T: BaseEntity
{
    /// <summary>
    /// Получить все сущности по заданному запросу.
    /// </summary>
    /// <param name="queryString">Запрос, содержащий настройки пагинации, сортировки и фильтрации.</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции.</param>
    /// <returns>Страница с набором сущностей с заданными в запросе параметрами.</returns>
    Task<PagedObject<T>> GetAllAsync(QueryStringParameters queryString, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции.</param>
    /// <returns>Сущность с заданным идентификатором.</returns>
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать сущность с заданными параметрами.
    /// </summary>
    /// <param name="entity">Создаваемая сущность.</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции.</param>
    /// <returns>Созданная сущность.</returns>
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить сущность с заданными параметрами.
    /// </summary>
    /// <param name="entity">Обновляемая сущность.</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции.</param>
    /// <returns>Обновленная сущность.</returns>
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить сущность с указанным идентификатором.
    /// </summary>
    /// <param name="id">Идентификтор удаляемой сущности.</param>
    /// <param name="cancellationToken">Токен отмены асинхронной операции.</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}