using ItPro.Data.Entities;
using ItPro.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItPro.Data.Configuration;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);

        builder
            .Property(order => order.Status)
            .HasConversion(x => Enum.GetName(x), y => Enum.Parse<Status>(y));
    }
}