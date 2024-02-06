using ItPro.Core.Exceptions;
using ItPro.Core.Helpful;
using ItPro.Core.Repository.Queries;
using ItPro.Data;
using ItPro.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItPro.Core.Repository;

public class BaseRepository<T> : IRepository<T> where T: BaseEntity
{
    protected readonly DataContext context;

    public BaseRepository(DataContext context)
    {
        this.context = context;
    }
    
    public virtual async Task<PagedObject<T>> GetAllAsync(QueryStringParameters queryString, CancellationToken cancellationToken = default)
    {
        var getQuery = this.context
            .Set<T>()
            .AsNoTracking()
            .ApplySorting(queryString.OrderBy, queryString.SortingOrder);

        return await getQuery.ToPagedListAsync(queryString.PageNumber, queryString.PageSize, cancellationToken);
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await this.context
            .Set<T>()
            .AsNoTracking()
            .Where(entity => entity.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (await this.context.Set<T>().AnyAsync(x => x.Id == entity.Id, cancellationToken))
        {
            throw new AlreadyExistsException($"Сущность с идентификатором {entity.Id} уже существует.");
        }
        
        this.context
            .Set<T>()
            .Add(entity);

        await this.context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (!await this.context.Set<T>().AnyAsync(x => x.Id == entity.Id, cancellationToken))
        {
            throw new NotFoundException($"Не удалось найти сущность с идентификатором {entity.Id}");
        }
        
        this.context
            .Set<T>()
            .Update(entity);

        await this.context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await this.context
            .Set<T>()
            .SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException($"Не удалось найти сущность с идентификатором {id}");
        }

        this.context
            .Set<T>()
            .Remove(entity);

        await this.context.SaveChangesAsync(cancellationToken);
    }
    
}