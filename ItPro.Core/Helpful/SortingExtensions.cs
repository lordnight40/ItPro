using System.Reflection;
using ItPro.Data.Entities;
using System.Linq.Dynamic.Core;

namespace ItPro.Core.Helpful;

public static class SortingExtensions
{
    public static IQueryable<T> ApplySorting<T>(
        this IQueryable<T> query,
        string sortingFieldName,
        SortingOrder direction) where T: BaseEntity
    {
        if (string.IsNullOrEmpty(sortingFieldName))
        {
            return query;
        }

        var propertyInfo = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .FirstOrDefault(info => info.Name.Equals(sortingFieldName, StringComparison.InvariantCultureIgnoreCase));
        
        if (propertyInfo is null)
        {
            return query;
        }

        var sortingOrder = direction switch
        {
            SortingOrder.Ascending => "ascending",
            SortingOrder.Descending => "descending",
            _ => "ascending"
        };

        return query.OrderBy($"{propertyInfo.Name} {sortingOrder}");
    }
}