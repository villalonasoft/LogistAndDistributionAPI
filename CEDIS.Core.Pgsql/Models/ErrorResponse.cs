using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.Models
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        public ErrorResponse(int error, string message)
        {
            Error = error;
            Message = message;
        }

        public int Error { get; set; }
        public string Message { get; set; }
    }
}
