using ItPro.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItPro.Data.Configuration;

public sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(client => client.Id);

        builder
            .HasMany(client => client.Orders)
            .WithOne(order => order.Client)
            .HasForeignKey()
            .OnDelete(DeleteBehavior.Restrict);
    }
}