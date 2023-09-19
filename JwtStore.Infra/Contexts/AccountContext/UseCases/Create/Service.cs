using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Create
{
    public class Service : IService
    {
        public Task SendEmailAsync(User user, CancellationToken cancellation)
        {
            // TODO
            return null;
        }
    }
}