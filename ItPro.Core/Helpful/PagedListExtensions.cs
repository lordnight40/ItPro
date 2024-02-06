using ItPro.Core.Repository;
using ItPro.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItPro.Core.Helpful;

public static class PagedListExtensions
{
    public static async Task<PagedObject<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default) where T: BaseEntity
    {
        var count = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedObject<T>(items, count, pageNumber, pageSize);
    }
}