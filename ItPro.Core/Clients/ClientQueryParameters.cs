using ItPro.Core.Repository.Queries;

namespace ItPro.Core.Clients;

public sealed class ClientQueryParameters : QueryStringParameters
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
    /// Нижняя граница даты рождения.
    /// </summary>
    public DateTime? BirthDayGreaterOrEquals { get; set; }
    
    /// <summary>
    /// Верхняя граница даты рождения.
    /// </summary>
    public DateTime? BirthDayLessOrEqual { get; set; }
    
    /// <summary>
    /// Точная дата рождения
    /// </summary>
    public DateTime? BirtDayEqual { get; set; }
}