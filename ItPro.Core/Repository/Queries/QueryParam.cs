using System.ComponentModel.DataAnnotations;
using ItPro.Core.Helpful;

namespace ItPro.Core.Repository.Queries;

/// <summary>
/// Объект запроса для постраничной выборки.
/// </summary>
public class QueryStringParameters
{
    /// <summary>
    /// Размер страницы.
    /// </summary>
    public int PageSize { get; set; } = 5;

    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Поле, по которому нужна сортировка.
    /// </summary>
    public string OrderBy { get; set; }

    /// <summary>
    /// Направление сортировки.
    /// </summary>
    /// <remarks>По умолчанию - по возрастанию.</remarks>
    public SortingOrder SortingOrder { get; set; } = SortingOrder.Ascending;
}