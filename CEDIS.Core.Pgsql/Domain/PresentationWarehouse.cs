using System.ComponentModel.DataAnnotations.Schema;

namespace CEDIS.Core.Pgsql.Domain
{
    public class PresentationWarehouse
    {
        public int WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        public int PresentationId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("PresentationId,ProductId")]
        public Presentation Presentation { get; set; }

        public int Pasillo { get; set; }
        public int Tramo { get; set; }
        public char Bandeja { get; set; }
        public int Ubitramo { get; set; }

        public int ZoneId { get; set; }

        [ForeignKey("WarehouseId,ZoneId")]
        public Zones Zones { get; set; }
    }
}
