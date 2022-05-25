using System.Collections.Generic;

namespace CEDIS.Core.Pgsql.Domain
{
    public class Status
    {
        public Enums.StatusEnum Id { get; set; }
        public string Name { get; set; }
        public string BackgroudColor { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
