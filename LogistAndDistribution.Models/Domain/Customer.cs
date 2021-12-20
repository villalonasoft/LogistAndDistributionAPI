using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogistAndDistribution.Models.Domain
{
    public class Customer
    {
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        public int PersonTypeId { get; set; }

        [ForeignKey("PersonTypeId")]
        public PersonType PersonType { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public int Id { get; set; }

        public string LargeName { get; set; }


        public int CreditDay { get; set; }

        public decimal CreditLimit { get; set; }
    }
}