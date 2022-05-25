using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.DTOs
{
    public class BranchOrderHeaderDetailDto
    {
        public int BranchId { get; set; }

        public int OrderId { get; set; }

        public int ZoneId { get; set; }

        public string CompleteCode { get => Code(); }

        public string ProductName { get; set; }

        public string Units { get; set; }

        public int QuantityAvailable { get; set; }

        public int CountedQuantity { get; set; }

        public string Location { get => FormatLocation(); }

        public string Zone { get; set; }

        public int PresentationId { get; set; }

        public int ProductId { get; set; }

        public int StatusId { get; set; }

        public string Status { get; set; }

        public decimal Cost { get; set; }

        public int Pasillo { get; set; }

        public int Tramo { get; set; }

        public char Bandeja { get; set; }

        public int Ubitramo { get; set; }

        private string Code()
        {
            return ProductId.ToString().PadLeft(6, '0') + PresentationId.ToString().PadLeft(2, '0');
        }

        private string FormatLocation()
        {
            var pasilloFormateado = string.Format("{0:00}", Pasillo);
            var tramoFornateado = string.Format("{0:00}", Tramo);
            var ubitramoFormateado = string.Format("{0:00}", Ubitramo);
            return $"P{pasilloFormateado}-{tramoFornateado}{Bandeja}{ubitramoFormateado}";
        }
    }
}
