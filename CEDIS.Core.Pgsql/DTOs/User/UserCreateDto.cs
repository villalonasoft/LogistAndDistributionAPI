using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.DTOs
{
    public class UserCreateDto
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool ChangePassword { get; set; } = false;
        public string NewPassword { get; set; } = "";
        public bool Status { get; set; } = true;
    }
}
