using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistenciaMongoDb.Model
{
    public class FullStockDTO
    {
        public string Sucursal { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public decimal Existencia { get; set; }
        public int NivelMax { get; set; }
    }
}
