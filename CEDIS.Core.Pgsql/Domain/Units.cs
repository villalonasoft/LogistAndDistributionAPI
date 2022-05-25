using System.ComponentModel.DataAnnotations;

namespace CEDIS.Core.Pgsql.Domain
{
    public class Units
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
