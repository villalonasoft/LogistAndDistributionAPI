using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistenciaMongoDb.Dto
{
    public class PostDto
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string ProductCode { get; set; }
        public int Existence { get; set; }
        public int ProductFactor { get; set; }
        public string ProductUnit { get; set; }
        public string ProductName { get; set; }
    }
}
