using Microsoft.EntityFrameworkCore;

namespace ItPro.Core.Helpful;

public static class PagedListExtensions
{
    /// <summary>
    /// Вспомогательный метод расширения к запросу для постраничного вывода данных
    /// </summary>
    /// <param name="source">Построитель запроса.</param>
    /// <param name="pageNumber">Номер запрашиваемой страницы.</param>
    /// <param name="pageSize">Размер запрашиваемой страницы.</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">Тип сущности, которые юудут в наборе.</typeparam>
    /// <returns>Страница с набором сущностей.</returns>
    public static async Task<PagedObject<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        
        var count = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedObject<T>(items, count, pageNumber, pageSize);
    }
}