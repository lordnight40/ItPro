namespace ItPro.Data.Entities;

/// <summary>
/// Элемент для хранения статистики среднего чека по часовым интервалам.
/// </summary>
public sealed class HourlyAverageReceiptSumStatistics
{
    /// <summary>
    /// Начало временного интервала.
    /// </summary>
    public string Start { get; set; }
    
    /// <summary>
    /// Средняя сумма чека.
    /// </summary>
    public decimal Average { get; set; }
}