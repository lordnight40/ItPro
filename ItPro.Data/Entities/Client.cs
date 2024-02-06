using System.ComponentModel.DataAnnotations;

namespace ItPro.Data.Entities;

/// <summary>
/// Сущность клиента.
/// </summary>
public class Client : BaseEntity
{
    /// <summary>
    /// Имя.
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    [Required]
    public string Surname { get; set; }
    
    /// <summary>
    /// Отчество.
    /// </summary>
    [Required]
    public DateTime BirthDay { get; set; }
    
    /// <summary>
    /// Заказы клиента.
    /// </summary>
    public ICollection<Order> Orders { get; set; }
}