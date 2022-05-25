using CEDIS.Core.Pgsql.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services.Interfaces
{
    public interface IBranchOrderService
    {
        Task<BranchOrderViewDto> AddAllAsync(int branchId, BranchOrderCreate orders);
        Task<BranchOrderViewDto> GetById(int id, int branchId);
        Task<BranchOrderViewDto> Publish(int branchId, int orderId, bool divider = false);
        Task<List<BranchOrderHeaderViewDto>> GetHeaders();
        Task<BranchOrderHeaderViewDto> GetOrderHeaderById(int id, int branchId, int zoneId);
        Task<bool> ChangeCenter(ChangeCenterDTO update);

        Task<IEnumerable<BranchOrderViewDto>> Get();
        Task<IEnumerable<BranchOrderViewDto>> GetAllByBranch(int branchId);
        Task<IEnumerable<BranchOrderViewDto>> GetByWarehouse(int warehouseId);
    }
}
