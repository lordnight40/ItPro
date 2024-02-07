using ItPro.Data.Entities;
using ItPro.Data.Enums;

namespace ItPro.Core.Statistics;

/// <summary>
/// Сервис получения статистики.
/// </summary>
public interface IOrderStatistics
{
    /// <summary>
    /// Получить сумму заказов со статусом выполнен по каждому клиенту, произведенных в день рождения клиента.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены асихнронной операции.</param>
    /// <returns>Данные статистики.</returns>
    Task<IEnumerable<BirthDaysReceiptStatistics>> GetBirthdayReceiptsStatisticsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить список часов от 00.00 до 24.00 в порядке убывания со средним чеком за каждый час (Средний чек=Сумма заказов/Кол-во заказов)
    /// по всем заказам со статусом Выполнен.
    /// </summary>
    /// <param name="status">Статус заказа.</param>
    /// <param name="cancellationToken">Токен отмены асихнронной операции.</param>
    /// <returns>Данные статистики.</returns>
    Task<IEnumerable<HourlyAverageReceiptSumStatistics>> GetHourlyAverageReceiptSumStatisticsAsync(
        Status status,
        CancellationToken cancellationToken = default);
}