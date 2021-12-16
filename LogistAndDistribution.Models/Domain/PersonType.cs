using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogistAndDistribution.Models.Domain
{
    public class PersonType
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
