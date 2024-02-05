namespace ItPro.Data.Entities;

/// <summary>
/// Базовая сущность. Содержит общие для сущностей поля.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
}