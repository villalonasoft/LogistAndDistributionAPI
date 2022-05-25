using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.DTOs
{
    public class BranchOrderViewDto
    {
        public int BranchId { get; set; }

        public int OrderId { get; set; }

        public string BranchName { get; set; }
        
        public string Reference { get; set; }
        
        public string Status { get; set; }

        public string BackgroudColor { get; set; }

        public int StatusId { get; set; }

        public string Date { get; set; }

        public string Mode { get; set; }

        public string Warehouse { get; set; }

        public bool CanOrder { get; set; }

        public ICollection<BranchOrderDetailViewDto> Detail { get; set; }
    }
}
