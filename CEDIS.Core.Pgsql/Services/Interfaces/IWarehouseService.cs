using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<Response<IEnumerable<Warehouse>>> GetAll();
        Task<Response<Warehouse>> GetById(int id);
        Task<Response<Warehouse>> Create(Warehouse warehouse);
        Task<bool> Update(int id, Warehouse warehouse);
    }
}
