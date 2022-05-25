using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.DTOs
{
    public class BranchOrderDetailViewDto
    {
        public string ProductCode { get=> GetCode(); }
        public int ProductId { get; set; }
        public int PresentationId { get; set; }
        public string ProductName { get; set; }

        public string Unit { get; set; }
        public int orderedQuantity { get; set; }

        public string Location { get=> FormatLocation(); }
        public string Zone { get; set; }

        public int Pasillo { get; set; }
        public int Tramo { get; set; }
        public char Bandeja { get; set; }
        public int Posicion { get; set; }
        public int Ubitramo { get; set; }

        private string GetCode()
        {
            return ProductId.ToString().PadLeft(6,'0')+PresentationId.ToString().PadLeft(2,'0');
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
