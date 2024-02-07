namespace ItPro.Core.Exceptions;

/// <summary>
/// Исключение для тех ситуаций, когда сущность отсутствует в БД.
/// </summary>
public sealed class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        
    }
}