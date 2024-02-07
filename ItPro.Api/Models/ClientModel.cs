namespace ItPro.Api.Models;

/// <summary>
/// Модель клиента для запроса к API.
/// </summary>
public sealed class ClientModel
{
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string Surname { get; set; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDay { get; set; }
}