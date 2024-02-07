using ItPro.Core.Helpful;
using ItPro.Core.Repository.Queries;
using ItPro.Data.Entities;
using ItPro.Data.Enums;

namespace ItPro.Core.Statistics;

public interface IOrderStatistics
{
    Task<IEnumerable<BirthDaysReceiptStatistics>> GetBirthdayReceiptsStatisticsAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<HourlyAverageReceiptSumStatistics>> GetHourlyAverageReceiptSumStatisticsAsync(
        Status status,
        CancellationToken cancellationToken = default);
}