namespace ItPro.Api.Models;

public sealed class OrderModel
{
    public Guid Id { get; set; }
    
    public decimal Amount { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid ClientId { get; set; }
}