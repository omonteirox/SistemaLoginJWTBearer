namespace JwtStore.Core.Contexts.AccountContext.Exceptions;

public class EmailException : Exception
{
    public EmailException(string message) : base(message)
    {
    }
}