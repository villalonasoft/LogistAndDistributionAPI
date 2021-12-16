using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogistAndDistribution.Models.Domain
{
    public class Presentation
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("ProductId,CompanyId")]
        public Product Product { get; set; }
        public string Name { get; set; }
    }
}
