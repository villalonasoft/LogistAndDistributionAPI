using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Persistences;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services
{
    public class PickService : IPickService
    {
        private readonly ApplicationDbContext _dbContext;

        public PickService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
