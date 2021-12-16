using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogistAndDistribution.Models.Domain
{
    public class OrderHeader
    {
        public int PersonTypeId { get; set; }
        public int PersonId { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("PersonTypeId,PersonId,CompanyId")]
        public Customer Customer { get; set; }
        public int Id { get; set; }

        public int OrderTypeId { get; set; }
        public OrderType OrderType { get; set; }
        public DateTime Date { get; set; }
        public DateTime? InitDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Priority { get; set; }

        public decimal Mount { get; set; }

        [MaxLength(25)]
        public string Reference { get; set; }
        public int Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
