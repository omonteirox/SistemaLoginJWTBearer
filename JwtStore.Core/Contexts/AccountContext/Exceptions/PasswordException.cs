namespace JwtStore.Core.Contexts.AccountContext.Exceptions
{
    public class PasswordException : Exception
    {
        public PasswordException(string message) : base(message)
        {
        }
    }
}