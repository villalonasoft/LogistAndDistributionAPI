using CEDIS.Core.Pgsql.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CEDIS.Core.Pgsql.Domain
{
    public class BoxOrder
    {
        public int WarehouseId { get; set; }
        public int BranchId { get; set; }
        public int OrderId { get; set; }
        public int ZoneId { get; set; }

        [ForeignKey("WarehouseId,BranchId,OrderId,ZoneId")]
        public OrderHeader OrderHeader { get; set; }

        public int BoxId { get; set; }

        [ForeignKey("WarehouseId,BoxId")]
        public Box Box { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public StatusBox StatusId { get; set; }
    }
}
