using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services.Interfaces
{
    public interface IModeService
    {
        Task<Response<Mode>> Update(int id, Mode mode);
        Task<Response<Mode>> Create(Mode mode);
        Task<Response<IEnumerable<Mode>>> GetResponseAsync();
    }
}
