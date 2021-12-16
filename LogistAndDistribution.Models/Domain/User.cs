using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogistAndDistribution.Models.Domain
{
    public class User
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
