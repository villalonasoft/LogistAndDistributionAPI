using System;
using System.Collections.Generic;
using System.Text;

namespace LogistAndDitribution.Core.Dto.OrdersDTO
{
    public class OrderSimpleViewDto
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string OrderType { get; set; }
        public string Date { get; set; }
        public string? InitDate { get; set; }
        public string? EndDate { get; set; }
        public int Priority { get; set; }
        public decimal Mount { get; set; }
        public string Reference { get; set; }
        public int Status { get; set; }

        public virtual ICollection<OrderDetailSimpleVIewDto> OrderDetails { get; set; }
    }
}
