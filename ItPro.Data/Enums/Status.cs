namespace ItPro.Data.Enums;

/// <summary>
/// Статусы заказов.
/// </summary>
public enum Status
{
    /// <summary>
    /// Не обработан.
    /// </summary>
    NotInProgress,
    
    /// <summary>
    /// Отменен.
    /// </summary>
    Canceled,
    
    /// <summary>
    /// Выполнен.
    /// </summary>
    Completed
}