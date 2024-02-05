using ItPro.Data.Entities;
using ItPro.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItPro.Data.Configuration;

/// <summary>
/// Конфигурация сущности клиента.
/// </summary>
public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);

        builder
            .Property(order => order.Status)
            .HasConversion(x => Enum.GetName(x), y => Enum.Parse<Status>(y));
    }
}