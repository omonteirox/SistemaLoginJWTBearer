namespace JwtStore.Core.Contexts.AccountContext.Exceptions
{
    public class VerificationException : Exception
    {
        public VerificationException(string? message) : base(message)
        {
        }
    }
}