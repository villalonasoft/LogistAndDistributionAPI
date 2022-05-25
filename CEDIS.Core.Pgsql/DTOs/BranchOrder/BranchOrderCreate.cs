using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.DTOs
{
    public class BranchOrderCreate
    {
        public int BranchId { get; set; }
        public int BranchReference { get; set; }
        public DateTime Date { get; set; }

        public int StatusId { get; set; } = 100;
        public string Mode { get; set; } = string.Empty;

        public int WarehouseId { get; set; }
        public List<BranchOrderDetailCreate> Details { get; set; }
    }
}
