using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogistAndDistribution.Models.Domain
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int InventoryFactor { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Unit? Child { get; set; }
    }
}
