using ItPro.Core.Helpful;

namespace ItPro.Core.Repository.Queries;

public class QueryStringParameters
{
    public int PageSize { get; set; }
    
    public int PageNumber { get; set; }
    
    public string OrderBy { get; set; }

    public SortingOrder SortingOrder { get; set; } = SortingOrder.Ascending;
}