using ItPro.Data.Enums;

namespace ItPro.Data.Entities;

/// <summary>
/// Сущность заказа.
/// </summary>
public sealed class Order : BaseEntity
{
    /// <summary>
    /// Сумма.
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Статус.
    /// </summary>
    public Status Status { get; set; }
    
    /// <summary>
    /// Клиент.
    /// </summary>
    public Client Client { get; set; }
}