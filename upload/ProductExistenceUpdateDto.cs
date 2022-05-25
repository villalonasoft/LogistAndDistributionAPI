using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upload
{
    public class ProductExistenceUpdateDto
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public decimal ProductCost { get; set; }
        public decimal Existence { get; set; }
        public int ProductFactor { get; set; }
        public int MaxStock { get; set; }
    }
}
