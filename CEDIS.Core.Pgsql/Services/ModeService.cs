using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Models;
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
    public class ModeService : IModeService
    {
        private readonly ApplicationDbContext _dbContext;

        public ModeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<IEnumerable<Mode>>> GetResponseAsync()
        {
            return new Response<IEnumerable<Mode>>(await _dbContext.Modes.OrderBy(x=>x.Id).ToListAsync());
        }

        public async Task<Response<Mode>> Create(Mode mode)
        {
            var result = _dbContext.Modes.Add(mode);
            return await _dbContext.SaveChangesAsync() > 0 ? new Response<Mode>(result.Entity) : new Response<Mode>(new ErrorResponse(400, "NO SE PUDO GUARDAR"));
        }

        public async Task<Response<Mode>> Update(int id, Mode mode)
        {
            var result = await _dbContext.Modes.FirstOrDefaultAsync(x => x.Id == id);
            result.Description = mode.Description;
            result.Abrebiature = mode.Abrebiature;
            result.Name = mode.Name;
            return await _dbContext.SaveChangesAsync() > 0 ? new Response<Mode>(mode) : new Response<Mode>(new ErrorResponse(400, "NO SE PUDO GUARDAR"));
        }
    }
}
