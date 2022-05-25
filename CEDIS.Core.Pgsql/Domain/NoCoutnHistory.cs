using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CEDIS.Core.Pgsql.Domain
{
    public class NoCoutnHistory
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        public string ProductName { get; set; }

        public string Cost { get; set; }

        [StringLength(25)]
        public string User { get; set; }

        public int Cant { get; set; }

        public int Stock { get; set; }

        public int Refpedido { get; set; }

        public int CountCant { get; set; }

        public string Date { get; set; }

        public string Status { get; set; }
    }
}
