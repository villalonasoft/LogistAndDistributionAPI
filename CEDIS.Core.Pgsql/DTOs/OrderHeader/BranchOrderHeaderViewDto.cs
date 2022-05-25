using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.DTOs
{
    public class BranchOrderHeaderViewDto
    {
        public int BranchId { get; set; }

        public int OrderId { get; set; }

        public int ZoneId { get; set; }

        public string BranchName { get; set; }

        public string BackgroudColor { get; set; }
        public string DateInit { get; set; }

        public string DateEnd { get; set; }

        public string User { get; set; }

        public string Status { get; set; }

        public string Zones { get; set; }

        public string Mode { get; set; }

        public ICollection<BranchOrderHeaderDetailDto> Details { get; set; }
    }
}
