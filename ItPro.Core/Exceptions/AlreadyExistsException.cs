namespace ItPro.Core.Exceptions;

public sealed class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string message) : base(message)
    {
        
    }
}