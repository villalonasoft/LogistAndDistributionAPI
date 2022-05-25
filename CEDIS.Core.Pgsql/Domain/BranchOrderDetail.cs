using System.ComponentModel.DataAnnotations.Schema;

namespace CEDIS.Core.Pgsql.Domain
{
    public class BranchOrderDetail
    {
        public int WarehouseId { get; set; }

        public int BranchId { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("WarehouseId,BranchId,OrderId")]
        public BranchOrder BranchOrder { get; set; }


        public int ProductId { get; set; }

        public int PresentationId { get; set; }

        [ForeignKey("WarehouseId,PresentationId,ProductId")]
        public PresentationWarehouse PresentationWarehouse { get; set; }

        public decimal Cost { get; set; }

        public int Factor { get; set; }

        public int UnitId { get; set; }

        [ForeignKey("UnitId")]
        public Units Unit { get; set; }
        public int OrderedQuantity { get; set; }
    }
}
