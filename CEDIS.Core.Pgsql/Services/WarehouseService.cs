using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Models;
using CEDIS.Core.Pgsql.Persistences;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly ApplicationDbContext _dbContext;

        public WarehouseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<IEnumerable<Warehouse>>> GetAll()
        {
            return new Response<IEnumerable<Warehouse>>(await _dbContext.Warehouses.ToListAsync());
        }

        public async Task<Response<Warehouse>> GetById(int id)
        {
            return new Response<Warehouse>(await _dbContext.Warehouses.Include(x => x.Zones).FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<Response<Warehouse>> Create(Warehouse warehouse)
        {
            warehouse.Zones= new List<Zones>() {
                new Zones {
                    Id=99,
                    Name="SIN ZONA",
                    FinPasillo=0,
                    FinTramo=0,
                    InitPasillo=0,
                    InitTramo=0
                }
            };

            var entity = _dbContext.Warehouses.Add(warehouse);
            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return new Response<Warehouse>(entity.Entity);
            }
            else
            {
                var error = new ErrorResponse { Error = 401, Message = $@"Cant Update Entity" };
                return new Response<Warehouse>(error);
            }
        }

        public async Task<bool> Update(int id, Warehouse warehouse)
        {
            var lasEntity = await _dbContext.Warehouses.Include(x => x.Zones).FirstOrDefaultAsync(x=>x.Id == id);
            if (lasEntity != null)
            {
                lasEntity.Name = warehouse.Name;
                lasEntity.Zones = warehouse.Zones;
            }
            return await _dbContext.SaveChangesAsync()>0;
        }
    }
}
