using ItPro.Data.Entities;

namespace ItPro.Core.Orders;

public static class OrderFilteringExtensions
{
    /// <summary>
    /// Применяет фильтрацию заказов к запросу.
    /// </summary>
    /// <param name="query">Строитель запроса.</param>
    /// <param name="queryParameters">Объект запроса.</param>
    /// <returns>Строитель запроса, дополненный фильтрацией.</returns>
    public static IQueryable<Order> ApplyFilter(this IQueryable<Order> query, OrderQueryParameters queryParameters)
    {
        // если нужно по указанному клиенту
        if (queryParameters.ClientIdEquals is not null)
        {
            query = query.Where(order => order.Client.Id == queryParameters.ClientIdEquals);
        }

        // если нужно по статусу
        if (queryParameters.Status is not null)
        {
            query = query.Where(order => order.Status == queryParameters.Status.Value);
        }

        // тут смотрим только даты
        query = queryParameters switch
        {
            // если нужна конкретная дата
            { CreatedEqual: not null } => query.Where(order => order.CreatedAt.Date == queryParameters.CreatedEqual.Value.Date),
            // до определенной даты
            { CreatedLessOrEqual: not null, CreatedGreaterOrEqual: null } => query.Where(order => order.CreatedAt < queryParameters.CreatedLessOrEqual),
            // после определенной даты
            { CreatedLessOrEqual: null, CreatedGreaterOrEqual: not null } => query.Where(order => order.CreatedAt >= queryParameters.CreatedGreaterOrEqual),
            // промежуток от и до
            { CreatedLessOrEqual: not null, CreatedGreaterOrEqual: not null } => query.Where(order => order.CreatedAt >= queryParameters.CreatedGreaterOrEqual && order.CreatedAt <= queryParameters.CreatedLessOrEqual),
            _ => query
        };

        // смотрим по стоимости
        query = queryParameters switch
        {
            // стоимость больше, чем
            {AmountGreaterOrEqual: not null, AmountLessOrEqual: null} => query.Where(order => order.Amount >= queryParameters.AmountGreaterOrEqual),
            // стоимость меньше, чем
            {AmountGreaterOrEqual: null, AmountLessOrEqual: not null} => query.Where(order => order.Amount <= queryParameters.AmountLessOrEqual),
            // стоимость в диапазоне от и до
            {AmountGreaterOrEqual: not null, AmountLessOrEqual: not null} => query.Where(order => order.Amount >= queryParameters.AmountGreaterOrEqual && order.Amount <= queryParameters.AmountLessOrEqual),
            _ => query
        };
        
        return query;
    }
}