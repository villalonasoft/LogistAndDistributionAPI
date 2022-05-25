using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.DTOs
{
    public class BranchOrderDetailCreate
    {
        public int ProductId { get; set; }

        public int PresentationId { get; set; }

        public decimal Cost { get; set; }

        public int Factor { get; set; }

        public int UnitId { get; set; }

        public int OrderedQuantity { get; set; }

    }
}
