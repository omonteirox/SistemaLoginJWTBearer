using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create
{
    public class Response : SharedContext.UseCases.Response
    {
        protected Response()
        { }

        public Response(string msg, int status, IEnumerable<String>? notifications = null)
        {
            Message = msg;
            Status = status;
            Notifications = notifications;
        }

        public Response(string msg, ResponseData data)
        {
            Message = msg;
            Status = 200;
            Data = data;
            Notifications = null;
        }

        public ResponseData? Data { get; set; }
    }
}

public record ResponseData(Guid Id, string Name, string Email);