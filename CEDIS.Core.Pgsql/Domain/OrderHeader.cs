using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CEDIS.Core.Pgsql.Domain
{
    public class OrderHeader
    {
        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("WarehouseId,BranchId, OrderId")]
        public BranchOrder BranchOrder { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime DateInit { get; set; }
        public DateTime DateEnd { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ConcurrencyCheck]
        public Enums.StatusEnum StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public int WarehouseId { get; set; }
        public int ZoneId { get; set; }

        [ForeignKey("WarehouseId,ZoneId")]
        public Zones Zones { get; set; }


        public int ModeId { get; set; }

        [ForeignKey("ModeId")]
        public Mode Mode { get; set; }
    }
}
