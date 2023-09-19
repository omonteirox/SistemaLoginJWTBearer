using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Create
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context) => _context = context;

        public async Task<bool> AnyAsync(string email, CancellationToken cancellation) => await _context.Users.AsNoTracking().AnyAsync(x => x.Email == email, cancellationToken: cancellation);

        public async Task SaveAsync(User user, CancellationToken cancellation)
        {
            await _context.Users.AddAsync(user, cancellation);
            await _context.SaveChangesAsync(cancellation);
        }
    }
}