namespace ItPro.Data.Entities;

/// <summary>
/// Элемент хранения статистики суммы заказов по каждому клиенту в дни рождения.
/// </summary>
public sealed class BirthDaysReceiptStatistics
{
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя клиента.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия клиента.
    /// </summary>
    public string Surname { get; set; }
    
    /// <summary>
    /// День рождения клиента.
    /// </summary>
    public DateTime BirthDay { get; set; }
    
    /// <summary>
    /// Сумма заказов.
    /// </summary>
    public decimal Sum { get; set; }
}