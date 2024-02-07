using ItPro.Data.Entities;

namespace ItPro.Core.Clients;

public static class ClientFilteringExtensions
{
    /// <summary>
    /// Применяет фильтрацию заказов к запросу.
    /// </summary>
    /// <param name="query">Строитель запроса.</param>
    /// <param name="queryParameters">Объект запроса.</param>
    /// <returns>Строитель запроса, дополненный фильтрацией.</returns>
    public static IQueryable<Client> ApplyFilter(this IQueryable<Client> query, ClientQueryParameters queryParameters)
    {
        ArgumentNullException.ThrowIfNull(query);
        
        // по имени
        if (!string.IsNullOrEmpty(queryParameters.Name))
        {
            query = query.Where(client => client.Name.Contains(queryParameters.Name, StringComparison.InvariantCultureIgnoreCase));
        }

        // по фамилии
        if (!string.IsNullOrEmpty(queryParameters.Surname))
        {
            query = query.Where(client => client.Name.Contains(queryParameters.Surname, StringComparison.InvariantCultureIgnoreCase));
        }

        // по дате рождения
        query = queryParameters switch
        {
            // по конкретной дате
            { BirtDayEqual: not null } => query.Where(client => client.BirthDay.Date == queryParameters.BirtDayEqual),
            // >= указанной дате
            { BirthDayGreaterOrEquals: not null, BirthDayLessOrEqual: null } => query.Where(client => client.BirthDay >= queryParameters.BirthDayGreaterOrEquals),
            // <= указанной дате
            { BirthDayGreaterOrEquals: null, BirthDayLessOrEqual: not null } => query.Where(client => client.BirthDay <= queryParameters.BirthDayLessOrEqual),
            // диапазон дат от и до
            { BirthDayGreaterOrEquals: not null, BirthDayLessOrEqual: not null } => query.Where(client => client.BirthDay >= queryParameters.BirthDayGreaterOrEquals && client.BirthDay <= queryParameters.BirthDayLessOrEqual),
            _ => query
        };

        return query;
    }
}