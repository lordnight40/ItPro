using ItPro.Core.Exceptions;
using ItPro.Core.Helpful;
using ItPro.Core.Repository;
using ItPro.Core.Repository.Queries;
using ItPro.Data;
using ItPro.Data.Entities;
using ItPro.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace ItPro.Core.Orders;

public sealed class OrderRepository : BaseRepository<Order>
{
    public OrderRepository(DataContext context) : base(context)
    {
    }
    
    public override async Task<PagedList<Order>> GetAllAsync(QueryStringParameters queryString, CancellationToken cancellationToken = default)
    {
        var getQuery = this.context.Orders
            .AsNoTracking()
            .ApplySorting(queryString.OrderBy, queryString.SortingOrder)
            .ApplyFilter(queryString as OrderQueryParameters);

        return await getQuery.ToPagedListAsync(queryString.PageNumber, queryString.PageSize, cancellationToken);
    }

    public override async Task<Order> CreateAsync(Order entity, CancellationToken cancellationToken = default)
    {
        if (await this.context.Orders.AnyAsync(x => x.Id == entity.Id, cancellationToken))
        {
            throw new AlreadyExistsException($"Сущность с идентификатором {entity.Id} уже существует.");
        }

        var client = await this.context.Clients
            .AsNoTracking()
            .SingleOrDefaultAsync(client => client.Id == entity.Id, cancellationToken);

        if (client is null)
        {
            throw new NotFoundException($"Пользователь с Id = {entity.Client.Id} для заказа не найден.");
        }

        entity.Client = client;
        entity.CreatedAt = DateTime.Now;
        entity.Status = Status.NotInProgress;
        
        this.context.Orders.Add(entity);
        
        await this.context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}