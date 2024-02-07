namespace ItPro.Core.Exceptions;

/// <summary>
/// Исключение для ситуаций, когда сущность уже имеется в БД.
/// </summary>
public sealed class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string message) : base(message)
    {
        
    }
}