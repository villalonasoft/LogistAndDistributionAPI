using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CEDIS.Core.Pgsql.Models
{
    public class WarehouseCredential
    {
        [Required]
        public string ApiKey { get; set; }
    }
}
