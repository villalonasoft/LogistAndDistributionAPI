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
    public class UnitService: IUnitService
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<IEnumerable<Units>>> GetResponseAsync()
        {
            return new Response<IEnumerable<Units>>(await _dbContext.Units.ToListAsync());
        }

        public async Task<Response<Units>> Create(Units units)
        {
            try
            {
                var result = _dbContext.Units.Add(units);
                return await _dbContext.SaveChangesAsync() > 0 ? new Response<Units>(result.Entity) : new Response<Units>(new ErrorResponse(400, "NO SE PUDO GUARDAR"));
            }
            catch (Exception ex)
            {
                return new Response<Units>(new ErrorResponse(ex.HResult, ex.Message));
            }
        }

        public async Task<Response<Units>> Update(int id, Units units)
        {
            try
            {
                var result = await _dbContext.Units.FirstOrDefaultAsync(x => x.Id == id);
                result.Description = units.Description;
                return await _dbContext.SaveChangesAsync() > 0 ? new Response<Units>(units) : new Response<Units>(new ErrorResponse(400, "NO SE PUDO GUARDAR"));
            }
            catch (Exception ex)
            {
                return new Response<Units>(new ErrorResponse(ex.HResult, ex.Message));
            }
        }
    }
}
