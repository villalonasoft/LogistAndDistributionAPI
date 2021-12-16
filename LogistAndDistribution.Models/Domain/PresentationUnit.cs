using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogistAndDistribution.Models.Domain
{
    public class PresentationUnit
    {
        public int PresentationId { get; set; }
        public int ProductId { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("PresentationId,ProductId,CompanyId")]
        public Presentation Presentation { get; set; }

        public int UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}
