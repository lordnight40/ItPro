namespace ItPro.Data.Enums;

/// <summary>
/// Статусы заказов.
/// </summary>
public enum Status
{
    /// <summary>
    /// Не обработан.
    /// </summary>
    NotInProgress = 0,
    
    /// <summary>
    /// Отменен.
    /// </summary>
    Canceled = 1,
    
    /// <summary>
    /// Выполнен.
    /// </summary>
    Completed = 2
}