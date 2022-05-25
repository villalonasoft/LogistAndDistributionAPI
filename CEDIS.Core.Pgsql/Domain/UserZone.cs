using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CEDIS.Core.Pgsql.Domain
{
    public class UserZone
    {
        public int UserId { get; set; }

        public int Id { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int WarehouseId { get; set; }
        public int ZoneId { get; set; }

        [ForeignKey("WarehouseId,ZoneId")]
        public Zones Zones { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public bool IsActive { get; set; }

    }
}
