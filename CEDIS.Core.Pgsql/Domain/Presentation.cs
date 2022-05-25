using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CEDIS.Core.Pgsql.Domain
{
    public class Presentation
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PresentationWarehouse> PresentationWarehouses { get; set; }

    }
}
