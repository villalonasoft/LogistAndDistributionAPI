using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.Models
{
    public class Response<T> where T:class
    {
        public Response()
        {
        }

        public Response(T response)
        {
            Data = response;
        }

        public Response(ErrorResponse error)
        {
            Error = error;
        }
        public T Data { get; set; }
        public ErrorResponse Error { get; set; }
    }
}
