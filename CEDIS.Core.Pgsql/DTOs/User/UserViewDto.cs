using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.DTOs
{
    public class UserViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
    }
}
