using JwtStore.Core.Contexts.AccountContext.Exceptions;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.SharedContext.Extensions;
using JwtStore.Core.Contexts.SharedContext.ValueObjects;
using System.Text.RegularExpressions;

namespace JwtStore.Core.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    private const string Pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

    protected Email()
    { }

    public Email(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new EmailException("E-mail inválido");
        Address = address.Trim().ToLower();
        if (address.Length < 5)
            throw new EmailException("E-mail inválido");
        if (!EmailRegex().IsMatch(address))
            throw new EmailException("E-mail inválido");
    }

    public string Address { get; }
    public string Hash => Address.ToBase64();

    public Verification VerificationCode { get; private set; } = new();

    public void ResendVerification()
    {
        VerificationCode = new();
    }

    public static implicit operator string(Email email) => email.ToString();

    public static implicit operator Email(string email) => new(email);

    public override string ToString() => Address.Trim().ToString();

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
}