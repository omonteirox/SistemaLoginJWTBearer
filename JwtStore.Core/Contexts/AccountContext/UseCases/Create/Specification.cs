using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create
{
    // vai ser feito o contrato.
    public class Specification
    {
        public static AbstractValidator<Request> Ensure(Request request) => new RequestValidator();

    ;
    }

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").Length(6, 20).WithMessage("Password must be between 6 and 20 characters");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").Length(1, 50).WithMessage("O campo nome deve ter entre 1 e 50 caracteres");
        }
    }
}