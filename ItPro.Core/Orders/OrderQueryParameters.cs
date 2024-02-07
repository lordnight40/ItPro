using ItPro.Core.Repository.Queries;
using ItPro.Data.Enums;

namespace ItPro.Core.Orders;

/// <inheritdoc/>
/// <remarks>
/// Содержит параметры для фильтрации.
/// Критерии выдуманные из головы.
/// </remarks>
public sealed class OrderQueryParameters : QueryStringParameters
{
    /// <summary>
    /// Статус заказа.
    /// </summary>
    public Status? Status { get; set; }
    
    /// <summary>
    /// Стоимость больше, либо равна.
    /// </summary>
    public decimal? AmountGreaterOrEqual { get; set; }
    
    /// <summary>
    /// Стоимость меньше, либо равна.
    /// </summary>
    public decimal? AmountLessOrEqual { get; set; }
    
    /// <summary>
    /// Дата создания больше, либо равна.
    /// </summary>
    public DateTime? CreatedGreaterOrEqual { get; set; }
    
    /// <summary>
    /// Дата создания меньше либо равна.
    /// </summary>
    public DateTime? CreatedLessOrEqual { get; set; }
    
    /// <summary>
    /// Дата создания равна.
    /// </summary>
    public DateTime? CreatedEqual { get; set; }
    
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public Guid? ClientIdEquals { get; set; }
}