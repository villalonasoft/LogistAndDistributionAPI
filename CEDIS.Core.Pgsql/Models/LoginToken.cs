using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.Models
{
    public class LoginToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
