using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Models;
using Picking.Core.Domain;
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
        TwoFactorAuthenticator GetSecretToQr();
        Task<bool> User2FAValid(int userId, string code);
        Task<bool> User2FACreate(int userId, TwoFactorAuthenticator twoFactorAuthenticator, string code);
    }
}
