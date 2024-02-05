using System.ComponentModel.DataAnnotations;
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
    [Required]
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Статус.
    /// </summary>
    [Required]
    [EnumDataType(typeof(Status))]
    public Status Status { get; set; }
    
    /// <summary>
    /// Клиент.
    /// </summary>
    [Required]
    public Client Client { get; set; }
}