using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.Models
{
    public class AssingOrderPost
    {
        public int ZoneId { get; set; }
        public int UserId { get; set; }
        public int ModeId { get; set; }
        public int WarehouseId { get; set; }
    }
}
