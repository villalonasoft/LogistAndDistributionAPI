using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CEDIS.Core.Pgsql.Domain
{
    public class Zones
    {
        public int WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse WareHouse { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int InitPasillo { get; set; }
        public int FinPasillo { get; set; }
        public int InitTramo { get; set; }
        public int FinTramo { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
