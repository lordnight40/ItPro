using ItPro.Core.Exceptions;
using ItPro.Core.Helpful;
using ItPro.Core.Repository;
using ItPro.Core.Repository.Queries;
using ItPro.Data;
using ItPro.Data.Entities;
using ItPro.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace ItPro.Core.Orders;

/// <summary>
/// Репозиторий для работы с заказами.
/// </summary>
public sealed class OrderRepository : BaseRepository<Order>
{
    public OrderRepository(DataContext context) : base(context)
    {
    }
    
    /// <inheritdoc/>
    public override async Task<PagedObject<Order>> GetAllAsync(QueryStringParameters queryString, CancellationToken cancellationToken = default)
    {
        var getQuery = this.context.Orders
            .AsNoTracking()
            .Include(order => order.Client)
            .ApplySorting(queryString.OrderBy, queryString.SortingOrder)
            .ApplyFilter(queryString as OrderQueryParameters);

        return await getQuery.ToPagedListAsync(queryString.PageNumber, queryString.PageSize, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<Order> CreateAsync(Order entity, CancellationToken cancellationToken = default)
    {
        if (await this.context.Orders.AnyAsync(x => x.Id == entity.Id, cancellationToken))
        {
            throw new AlreadyExistsException($"Сущность с идентификатором {entity.Id} уже существует.");
        }

        var client = await this.context.Clients
            .SingleOrDefaultAsync(client => client.Id == entity.Client.Id, cancellationToken);

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

    /// <inheritdoc/>
    public override async Task<Order> UpdateAsync(Order entity, CancellationToken cancellationToken = default)
    {
        var order = await this.context.Orders
            .Include(order => order.Client)
            .SingleOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
            
        if (order is null)
        {
            throw new NotFoundException($"Не удалось найти сущность с идентификатором {entity.Id}");
        }

        order.CreatedAt = entity.CreatedAt;
        order.Status = entity.Status;
        order.Amount = entity.Amount;

        if (order.Client.Id != entity.Client.Id)
        {
            var client = await this.context.Clients.SingleOrDefaultAsync(client => client.Id == entity.Client.Id, cancellationToken);

            if (client is null)
            {
                throw new NotFoundException($"Пользователь с Id = {entity.Client.Id} для заказа не найден.");
            }
            
            order.Client = client;
        }

        await this.context.SaveChangesAsync(cancellationToken);

        return order;
    }
}