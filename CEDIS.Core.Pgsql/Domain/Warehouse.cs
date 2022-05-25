using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CEDIS.Core.Pgsql.Domain
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public virtual ICollection<Zones> Zones { get; set; }
    }
}
