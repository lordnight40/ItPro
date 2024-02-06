using ItPro.Data.Entities;

namespace ItPro.Core.Clients;

public static class ClientFilteringExtensions
{
    public static IQueryable<Client> FilterBy(this IQueryable<Client> query, ClientQueryParameters queryParameters)
    {
        ArgumentNullException.ThrowIfNull(query);
        
        if (!string.IsNullOrEmpty(queryParameters.Name))
        {
            query = query.Where(client => client.Name.Contains(queryParameters.Name, StringComparison.InvariantCultureIgnoreCase));
        }

        if (!string.IsNullOrEmpty(queryParameters.Surname))
        {
            query = query.Where(client => client.Name.Contains(queryParameters.Surname, StringComparison.InvariantCultureIgnoreCase));
        }

        if (queryParameters.BirtDayAt is not null)
        {
            query = query.Where(client => client.BirthDay.Date == queryParameters.BirtDayAt.Value);
        }
        else
        {
            (DateTime? from, DateTime? to) dates = (queryParameters.BirthDayGreaterOrEquals, queryParameters.BirthDayLessOrEqual);

            query = dates switch
            {
                (from: null, to: not null) => query.Where(client => client.BirthDay <= dates.to),
                (from: not null, to: null) => query.Where(client => client.BirthDay >= dates.from),
                (from: not null, to: not null) => query.Where(client => client.BirthDay >= dates.from && client.BirthDay <= dates.to),
                _ => query
            };
        }

        return query;
    }
}