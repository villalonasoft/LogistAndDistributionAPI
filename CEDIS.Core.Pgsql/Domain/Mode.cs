using System.Collections.Generic;

namespace CEDIS.Core.Pgsql.Domain
{
    public class Mode
    {
        public int Id { get; set; }
        public char Abrebiature { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
