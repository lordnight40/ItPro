namespace ItPro.Data.Entities;

/// <summary>
/// Сущность клиента.
/// </summary>
public sealed class Client : BaseEntity
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string Surname { get; set; }
    
    /// <summary>
    /// Отчество.
    /// </summary>
    public DateOnly BirthDay { get; set; }
    
    /// <summary>
    /// Заказы клиента.
    /// </summary>
    public ICollection<Order> Orders { get; set; }
}