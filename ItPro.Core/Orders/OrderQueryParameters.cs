using ItPro.Core.Repository.Queries;
using ItPro.Data.Enums;

namespace ItPro.Core.Orders;

public sealed class OrderQueryParameters : QueryStringParameters
{
    public Status? Status { get; set; }
    
    public decimal? AmountGreaterOrEqual { get; set; }
    
    public decimal? AmountLessOrEqual { get; set; }
    
    public DateTime? CreatedGreaterOrEqual { get; set; }
    
    public DateTime? CreatedLessOrEqual { get; set; }
    
    public DateTime? CreatedEqual { get; set; }
    
    public Guid? ClientIdEquals { get; set; }
}