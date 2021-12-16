using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogistAndDistribution.Models.Domain
{
    public class Zone
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string HallInit { get; set; }
        public string HallEnd { get; set; }
    }
}
