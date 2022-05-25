using AutoMapper;
using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Models;
using CEDIS.Core.Pgsql.Persistences;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<User> Authenticate(Credential credential)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(us => us.UserName.ToUpper().Trim() == credential.Username.ToUpper().Trim());

            if (user == null)
                return null;

            bool validPassword = BCrypt.Net.BCrypt.Verify(credential.Password, user.Password);

            if (!validPassword)
                return null;

            return user;
        }

        public async Task<Warehouse> Authenticate(WarehouseCredential credential)
        {

            if (credential == null)
                throw new Exception("Credentials was not provided.");

            var apiKey = await _dbContext.Warehouses.Where(apk => apk.ApiKey == credential.ApiKey)
                                                        .FirstOrDefaultAsync();
            return apiKey ?? null;
        }

        public async Task<Branch> Authenticate(BranchCredential credential)
        {

            if (credential == null)
                throw new Exception("Credentials was not provided.");

            var apiKey = await _dbContext.Branches.Where(apk => apk.ApiKey == credential.ApiKey)
                                                        .FirstOrDefaultAsync();

            return apiKey ?? null;
        }


    }
}
