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
    
    public async Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await this.context
            .Set<T>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        return result.AsReadOnly();
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await this.context
            .Set<T>()
            .AsNoTracking()
            .Where(entity => entity.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        this.context
            .Set<T>()
            .Add(entity);

        await this.context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
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

        this.context
            .Set<T>()
            .Remove(entity);

        await this.context.SaveChangesAsync(cancellationToken);
    }
}