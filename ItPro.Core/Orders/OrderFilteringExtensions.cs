using ItPro.Data.Entities;

namespace ItPro.Core.Orders;

public static class OrderFilteringExtensions
{
    public static IQueryable<Order> ApplyFilter(this IQueryable<Order> query, OrderQueryParameters queryParameters)
    {
        if (queryParameters.ClientIdEquals is not null)
        {
            query = query.Where(order => order.Client.Id == queryParameters.ClientIdEquals);
        }

        if (queryParameters.Status is not null)
        {
            query = query.Where(order => order.Status == queryParameters.Status.Value);
        }

        // тут смотрим только даты
        query = queryParameters switch
        {
            { CreatedEqual: not null } => query.Where(order => order.CreatedAt == queryParameters.CreatedEqual),
            { CreatedLessOrEqual: not null, CreatedGreaterOrEqual: null } => query.Where(order => order.CreatedAt < queryParameters.CreatedLessOrEqual),
            { CreatedLessOrEqual: null, CreatedGreaterOrEqual: not null } => query.Where(order => order.CreatedAt >= queryParameters.CreatedGreaterOrEqual),
            { CreatedLessOrEqual: not null, CreatedGreaterOrEqual: not null } => query.Where(order => order.CreatedAt >= queryParameters.CreatedGreaterOrEqual && order.CreatedAt <= queryParameters.CreatedLessOrEqual),
            _ => query
        };

        query = queryParameters switch
        {
            {AmountGreaterOrEqual: not null, AmountLessOrEqual: null} => query.Where(order => order.Amount >= queryParameters.AmountGreaterOrEqual),
            {AmountGreaterOrEqual: null, AmountLessOrEqual: not null} => query.Where(order => order.Amount <= queryParameters.AmountLessOrEqual),
            {AmountGreaterOrEqual: not null, AmountLessOrEqual: not null} => query.Where(order => order.Amount >= queryParameters.AmountGreaterOrEqual && order.Amount <= queryParameters.AmountLessOrEqual),
            _ => query
        };
        
        return query;
    }
}