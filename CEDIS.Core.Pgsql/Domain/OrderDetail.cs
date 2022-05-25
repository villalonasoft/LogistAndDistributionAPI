using CEDIS.Core.Pgsql.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CEDIS.Core.Pgsql.Domain
{
    public class OrderDetail
    {
        public int BranchId { get; set; }

        public int OrderId { get; set; }

        public int ZoneId { get; set; }
        public int WarehouseId { get; set; }

        [ForeignKey("WarehouseId,BranchId,OrderId,ZoneId")]
        public OrderHeader OrderHeader { get; set; }

        public int PresentationId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("WarehouseId,PresentationId,ProductId")]
        public PresentationWarehouse PresentationWarehouse { get; set; }

        public StatusDetailEnum StatusId { get; set; }
        [ForeignKey("StatusId")]
        public StatusDetail Status { get; set; }

        public decimal Cost { get; set; }

        public int UnitId { get; set; }

        [ForeignKey("UnitId")]
        public Units Units { get; set; }

        public int QuantityAvailable { get; set; }
        public int CountedQuantity { get; set; }
    }
}
