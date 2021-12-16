using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LogistAndDistribution.Models.Domain
{
    public class OrderDetail
    {
        public int ZoneId { get; set; }
        public int UnitId { get; set; }
        public int PresentationId { get; set; }
        public int ProductId { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("ZoneId,UnitId,PresentationId,ProductId,CompanyId")]
        public Stock Stock { get; set; }

        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId,CompanyId")]
        public OrderHeader OrderHeader { get; set; }
        public int CuantityOrder { get; set; }
        public int CuantityPicked { get; set; }
    }
}
