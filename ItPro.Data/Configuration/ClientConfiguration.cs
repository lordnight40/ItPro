using ItPro.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItPro.Data.Configuration;

/// <summary>
/// Конфигурация сущности клиента.
/// </summary>
public sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(client => client.Id);

        // Устанавливаем связи
        builder
            .HasMany(client => client.Orders)
            .WithOne(order => order.Client)
            // Лучше оставить Restrict, т.к. по умолчания Cascade, а каскадное удаление на практике редко нужно
            .OnDelete(DeleteBehavior.Restrict);
    }
}