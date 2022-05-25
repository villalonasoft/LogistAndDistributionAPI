using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services.Interfaces
{
    public interface IBranchServices
    {
        Task<Branch> GetBranchId(string apiKey);
        Task<IEnumerable<Warehouse>> GetWarehouse();
        Task<IEnumerable<BranchViewDto>> GetBranches();
        Task<BranchViewDto> GetBranchById(int id);
        Task<bool> CreateBranch(BranchViewDto branch);
        Task<bool> UpdateBranch(int id,BranchViewDto branch);
    }
}
