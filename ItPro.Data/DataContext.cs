using System.Reflection;
using ItPro.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItPro.Data;

/// <summary>
/// Контекст данных приложения.
/// </summary>
public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    
    /// <summary>
    /// Клиенты.
    /// </summary>
    public DbSet<Client> Clients { get; set; }
    
    /// <summary>
    /// Заказы.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder
            .Entity<BirthDaysReceiptStatistics>()
            .HasNoKey()
            .ToView(null);
        
        modelBuilder
            .Entity<HourlyAverageReceiptSumStatistics>()
            .HasNoKey()
            .ToView(null);
    }
}