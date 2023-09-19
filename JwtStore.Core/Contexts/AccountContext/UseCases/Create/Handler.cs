using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.Exceptions;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create
{
    public class Handler
    {
        private readonly IRepository _repository;
        private readonly IService _service;

        public Handler(IRepository repository, IService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellation)
        {
            #region 01. Valida Requisição

            try
            {
                var res = Specification.Ensure(request).Validate(request);
                if (!res.IsValid)
                    new Response("Invalid request", 400, res.Errors.Select(x => x.ErrorMessage).ToList());
            }
            catch (InvalidRequestException ex)
            {
                new Response("Não foi possível fazer a requisição", 500);
            }

            #endregion 01. Valida Requisição

            #region 02. Gerar os objetos

            Email email;
            Password password;
            User user;
            try
            {
                email = new Email(request.Email);
                password = new Password(request.Password);
                user = new User(request.Name, email, password);
            }
            catch (Exception ex)
            {
                new Response(ex.Message, 500);
            }

            #endregion 02. Gerar os objetos

            #region 03. Verifica se existe no banco

            try
            {
                var userExists = await _repository.AnyAsync(request.Email, cancellation);
                if (userExists)
                    new Response("Usuário já existe", 400);
            }
            catch (DatabaseException ex)
            {
                new Response("Falha ao verificar o e-mail cadastrado", 500);
            }

            #endregion 03. Verifica se existe no banco

            #region 04. Cria o usuário

            try
            {
                await _repository.SaveAsync(user, cancellation);
            }
            catch (DatabaseException ex)
            {
                new Response("Falha ao persistir os dados", 500);
            }

            #endregion 04. Cria o usuário

            #region 05. Envia o e-mail

            try
            {
                await _service.SendEmailAsync(user, cancellation);
            }
            catch (EmailException ex)
            {
                // faz nada
            }

            #endregion 05. Envia o e-mail

            return new Response("Usuário criado com sucesso", new ResponseData(user.Id, user.Name, user.Email));
        }
    }
}