using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services.Interfaces
{
    public interface IUnitService
    {
        Task<Response<IEnumerable<Units>>> GetResponseAsync();
        Task<Response<Units>> Create(Units units);
        Task<Response<Units>> Update(int id, Units units);
    }
}
