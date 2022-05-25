using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CEDIS.Core.Pgsql.Models
{
    public class BranchCredential
    {
        [Required]
        public string ApiKey { get; set; }
    }
}
