using JwtStore.Core.AccountContext.Exceptions;
using JwtStore.Core.AccountContext.ValueObjects;

namespace JwtStore.Core.AccountContext.Entities;

public class User : Entity
{
    protected User()
    { }

    public User(string email, string password = null)
    {
        Email = email;
        Password = new Password(password);
    }

    public Email Email { get; private set; } = string.Empty;
    public Password Password { get; set; } = null!;
    public string Name { get; private set; } = null!;

    public string Image { get; set; } = string.Empty;

    public void UpdatePassword(string plainTextPassword, string code)
    {
        if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new PasswordException("Invalid code");
        var password = new Password(plainTextPassword);
        Password = password;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }

    public void ChangePassword(string plainTextPassword)
    {
        var password = new Password(plainTextPassword);
        Password = password;
    }
}