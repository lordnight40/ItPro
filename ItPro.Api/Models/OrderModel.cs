namespace ItPro.Api.Models;

/// <summary>
/// Модель заказа для запроса к API.
/// </summary>
public sealed class OrderModel
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Сумма заказа.
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Дата создания заказа.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public Guid ClientId { get; set; }
    
    /// <summary>
    /// Клиент. Навигационное свойство.
    /// </summary>
    public ClientModel Client { get; set; }
}