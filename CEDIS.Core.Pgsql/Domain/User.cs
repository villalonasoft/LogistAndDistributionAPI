
using Picking.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace CEDIS.Core.Pgsql.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        public string Salt { get; set; }
        public string Password { get; set; }

        public int? TwoFactorAutenticationId { get; set; }

        [ForeignKey("TwoFactorAutenticationId")]
        public TwoFactorAuthenticator? TwoFactorAuthenticator { get; set; }
        public bool Status { get; set; }
    }
}
