using CEDIS.Core.Pgsql.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CEDIS.Core.Pgsql.Domain
{
    public class BranchOrder
    {
        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public int OrderId { get; set; }

        public string Reference { get; set; }

        public int WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        [Required]
        public StatusEnum StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        public DateTime Date { get; set; }

        public string Mode { get; set; }

        public virtual List<BranchOrderDetail> BranchOrderDetails { get; set; }

        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }

    }
}
