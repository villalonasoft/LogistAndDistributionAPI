using System;
using System.Collections.Generic;
using System.Text;

namespace LogistAndDitribution.Core.Dto.OrdersDTO
{
    public class OrderDetailSimpleVIewDto
    {
        public string Zone { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
        public string CompleteCode { get => ProductId.ToString().PadLeft(6, '0') + PresentationId.ToString().PadLeft(2, '0'); }
        public int PresentationId { get; set; }
        public int ProductId { get; set; }
        public decimal Stock { get; set; }
        public int CuantityOrder { get; set; }
        public int CuantityPicked { get; set; }
    }
}
