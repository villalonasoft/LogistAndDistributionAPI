using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CEDIS.Core.Pgsql.Frameworks.Extention
{
    public static class HttpContextUserClaimExtension
    {
        public static Warehouse GetAuthorizedWarehouse(this ClaimsPrincipal claimsPrincipal)
        {
            var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;

            return new Warehouse
            {
                Id = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value)
            };
        }

        public static User GetAuthorizedUser(this ClaimsPrincipal claimsPrincipal)
        {
            var userType = GetLogInType(claimsPrincipal);

            if (userType == UserLogInType.User)
            {
                var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
                return new User
                {
                    Id = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value)
                };
            }
            else
            {
                throw new Exception("There is not an authorized user.");
            }
        }

        public static Branch GetAuthorizedBranch(this ClaimsPrincipal claimsPrincipal)
        {
            var userType = GetLogInType(claimsPrincipal);

            if (userType == UserLogInType.Branch)
            {
                var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;

                    var branchId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);

                    return new Branch
                    {
                        Id = branchId
                    };
            }
            else
            {
                throw new Exception("There is not an authorized branch.");
            }
        }

        public static bool IsAdminUser(this ClaimsPrincipal claimsPrincipal)
        {
            //var LogInType = GetLogInType(claimsPrincipal);

            //if (LogInType == UserLogInType.User)
            //{
            //    var user = GetAuthorizedUser(claimsPrincipal);

            //    if (user.UserType == UserType.IsAdmin)
            //        return true;
            //}

            return false;
        }

        public static bool IsAdminCompany(this ClaimsPrincipal claimsPrincipal)
        {
            //var currentCompanyType =
            //    (CompanyTypes)Convert.ToInt32(claimsPrincipal.FindFirst(CustomClaimsType.CompanyType)?.Value);

            //if (currentCompanyType == CompanyTypes.Admin)
            //    return true;

            return false;
        }

        #region Helper methods

        public static UserLogInType GetLogInType(this ClaimsPrincipal claimsPrincipal)
        {
            var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
            var LogInType = claimsIdentity.FindFirst(CustomClaimsType.LogInType)?.Value;

            if (Enum.TryParse(typeof(UserLogInType), LogInType, out var result))
                return (UserLogInType)result;

            throw new Exception("No se encontró un tipo de usuario en el token.");
        }

        #endregion
    }
}
