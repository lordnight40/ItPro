using ItPro.Core.Exceptions;
using ItPro.Core.Helpful;
using ItPro.Core.Repository;
using ItPro.Core.Repository.Queries;
using ItPro.Data;
using ItPro.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItPro.Core.Clients;

public sealed class ClientRepository : BaseRepository<Client>
{
    public ClientRepository(DataContext context) : base(context)
    {
    }

    public override async Task<PagedObject<Client>> GetAllAsync(QueryStringParameters queryString, CancellationToken cancellationToken = default)
    {
        var getQuery = this.context.Clients
            .AsNoTracking()
            .ApplySorting(queryString.OrderBy, queryString.SortingOrder)
            .FilterBy(queryString as ClientQueryParameters);

        return await getQuery.ToPagedListAsync(queryString.PageNumber, queryString.PageSize, cancellationToken);
    }

    public override async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await this.context.Clients
            .Include(client => client.Orders)
            .SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException($"Не удалось найти сущность с идентификатором {id}");
        }

        this.context.Orders.RemoveRange(entity.Orders);
        this.context.Clients.Remove(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}