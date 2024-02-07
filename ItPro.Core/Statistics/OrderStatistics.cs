using ItPro.Core.Repository.Queries;
using ItPro.Data;
using ItPro.Data.Entities;
using ItPro.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace ItPro.Core.Statistics;

public sealed class OrderStatistics : IOrderStatistics
{
    private readonly DataContext context;

    public OrderStatistics(DataContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<BirthDaysReceiptStatistics>> GetBirthdayReceiptsStatisticsAsync(QueryStringParameters parameters, CancellationToken cancellationToken = default)
    {
        return await this.context
            .Set<BirthDaysReceiptStatistics>()
            .FromSqlRaw("EXEC [dbo].[GetBirthdayReceiptSumByClients]")
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<HourlyAverageReceiptSumStatistics>> GetHourlyAverageReceiptSumStatisticsAsync(Status status, CancellationToken cancellationToken = default)
    {
        return await this.context
            .Set<HourlyAverageReceiptSumStatistics>()
            .FromSqlInterpolated($"EXEC [dbo].[GetHourlyAverageSumByStatus] {Enum.GetName(status)}")
            .ToListAsync(cancellationToken);
    }
}