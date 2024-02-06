using System.ComponentModel.DataAnnotations;
using ItPro.Core.Helpful;

namespace ItPro.Core.Repository.Queries;

public class QueryStringParameters
{
    public int PageSize { get; set; } = 5;

    public int PageNumber { get; set; } = 1;
    
    public string OrderBy { get; set; }

    [Display]
    public SortingOrder SortingOrder { get; set; } = SortingOrder.Ascending;
}