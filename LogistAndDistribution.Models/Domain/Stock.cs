using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogistAndDistribution.Models.Domain
{
    public class Stock
    {
        public int UnitId { get; set; }
        public int PresentationId { get; set; }
        public int ProductId { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("UnitId,PresentationId,ProductId,CompanyId")]
        public PresentationUnit Presentation { get; set; }

        public int ZoneId { get; set; }
        [ForeignKey("ZoneId,CompanyId")]
        public Zone Zone { get; set; }
        public decimal Cant { get; set; }
    }
}
