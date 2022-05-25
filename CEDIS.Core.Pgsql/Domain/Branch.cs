using System.Collections.Generic;

namespace CEDIS.Core.Pgsql.Domain
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public Schedule Schedule { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
