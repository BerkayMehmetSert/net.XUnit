namespace Core.Exceptions.Types;

public class InternalServerException : Exception
{
    public InternalServerException(string message) : base(message)
    {
    }
}