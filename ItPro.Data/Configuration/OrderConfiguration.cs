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

        // Для более красивого хранения перечисления в базе настраиваем конвертацию
        // Строка более красиво и понятно смотрится, чем число, имхо
        builder
            .Property(order => order.Status)
            .HasConversion(x => Enum.GetName(x), y => Enum.Parse<Status>(y));
    }
}