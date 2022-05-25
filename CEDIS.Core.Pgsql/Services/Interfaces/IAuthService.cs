using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Authenticate(Credential credential);
        Task<Warehouse> Authenticate(WarehouseCredential credential);
        Task<Branch> Authenticate(BranchCredential credential);
    }
}
