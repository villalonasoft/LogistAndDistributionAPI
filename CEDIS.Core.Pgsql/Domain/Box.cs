using System.ComponentModel.DataAnnotations.Schema;

namespace CEDIS.Core.Pgsql.Domain
{
    public class Box
    {
        public int WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
        public int Id { get; set; }
    }
}
