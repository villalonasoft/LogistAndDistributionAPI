using AutoMapper;
using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.DTOs;
using CEDIS.Core.Pgsql.Models;
using CEDIS.Core.Pgsql.Persistences;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services
{
    public class BranchServices : IBranchServices
    {
        private readonly ApplicationDbContext _pickingdbContext;
        private readonly IMapper _mapper;

        public BranchServices(ApplicationDbContext pickingdbContext,IMapper mapper)
        {
            _pickingdbContext = pickingdbContext;
            _mapper = mapper;
        }

        public async Task<Branch> GetBranchId(string apiKey)
        {
            return await _pickingdbContext.Branches.FirstOrDefaultAsync(x => x.ApiKey.Trim() == apiKey.Trim());
        }

        public async Task<IEnumerable<Warehouse>> GetWarehouse()
        {
            return await _pickingdbContext.Warehouses.ToListAsync();
        }

        public async Task<IEnumerable<BranchViewDto>> GetBranches(){

            try
            {
                return _mapper.Map<IEnumerable<BranchViewDto>>(await _pickingdbContext.Branches.Include(x => x.Schedule).ToListAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BranchViewDto> GetBranchById(int id)
        {
            return _mapper.Map<BranchViewDto>(await _pickingdbContext.Branches.Include(x => x.Schedule).FirstOrDefaultAsync(x=>x.Id==id));
        }

        public async Task<bool> CreateBranch(BranchViewDto branch)
        {
            var newBranch = _mapper.Map<Branch>(branch);
            _pickingdbContext.Branches.Add(newBranch);
            return await _pickingdbContext.SaveChangesAsync()>0;
        }

        public async Task<bool> UpdateBranch(int id, BranchViewDto branch)
        {
            try
            {
                var oldBranch = await _pickingdbContext.Branches.Include(x => x.Schedule).FirstOrDefaultAsync(x => x.Id == id);
                var newBranch = _mapper.Map<BranchViewDto, Branch>(branch, oldBranch);
                if (await _pickingdbContext.SaveChangesAsync() > 0)
                    return true;
                throw new Exception(JsonConvert.SerializeObject(new ErrorResponse() { Error = 418, Message = "No se pudo Actualizar " + branch.Name }));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
