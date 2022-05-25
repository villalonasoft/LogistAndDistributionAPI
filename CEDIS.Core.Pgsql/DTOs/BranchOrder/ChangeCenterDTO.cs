using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.DTOs
{
    public class ChangeCenterDTO
    {
        public int BranchId { get; set; }
        public int OrderId { get; set; }
        public int WarehouseId { get; set; }
    }
}
