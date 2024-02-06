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

    public override async Task<PagedList<Client>> GetAllAsync(QueryStringParameters queryString, CancellationToken cancellationToken = default)
    {
        var getQuery = this.context.Clients
            .AsNoTracking()
            .ApplySorting(queryString.OrderBy, queryString.SortingOrder)
            .FilterBy(queryString as ClientQueryParameters);

        return await getQuery.ToPagedListAsync(queryString.PageNumber, queryString.PageSize, cancellationToken);
    }
}