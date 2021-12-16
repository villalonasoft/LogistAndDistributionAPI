using System;
using System.Collections.Generic;
using System.Text;

namespace LogistAndDitribution.Core.Dto.OrdersDTO
{
    public class OrderPostDto
    {
        public int UnitId { get; set; }
        public int PresentationId { get; set; }
        public int ProductId { get; set; }
        public int CuantityOrder { get; set; }
    }
}
