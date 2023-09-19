using JwtStore.Core.Contexts.AccountContext.Exceptions;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects
{
    public class Verification
    {
        public string Code { get; } = Guid.NewGuid().ToString("N")[0..6].ToUpper();
        public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);

        public DateTime? VerifiedAt { get; private set; } = null;
        public bool IsActive => VerifiedAt != null && ExpiresAt == null;

        public Verification()
        { }

        public void Verify(string code)
        {
            if (IsActive)
                throw new VerificationException("Verification code is already active");
            if (ExpiresAt < DateTime.UtcNow)
                throw new VerificationException("Verification code is expired");
            if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
                throw new VerificationException("Verification code is invalid");
            ExpiresAt = null;
            VerifiedAt = DateTime.UtcNow;
        }
    }
}