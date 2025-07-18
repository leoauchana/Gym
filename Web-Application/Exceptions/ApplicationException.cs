namespace Web_Application.Exceptions;

public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message) { }
}
