
namespace CEDIS.Core.Pgsql.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        public string Salt { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}
