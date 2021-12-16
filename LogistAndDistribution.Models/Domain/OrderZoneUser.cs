using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogistAndDistribution.Models.Domain
{
    public class OrderZoneUser
    {
        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId,CompanyId")]
        public OrderHeader OrderHeader { get; set; }
        public int ZoneId { get; set; }

        [ForeignKey("ZoneId,CompanyId")]
        public Zone Zone { get; set; }

        public Nullable<int> UserId { get; set; }

        [ForeignKey("UserId,CompanyId")]
        public User User { get; set; }
        public int CompanyId { get; set; }

        public bool IsFinish { get; set; } = false;
    }
}
