using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services.Interfaces
{
    public interface IPickingService
    {
        Task<OrderHeader> RandomOrder(int userId, AssingOrderPost assingOrder);
        Task<OrderHeader> PickOrder(int branchId, int orderId, int zoneId, int userId, int warehouseId);
        Task<IEnumerable<OrderHeader>> MyOrders(int UserId);
        Task<OrderHeader> GetOrderDetail(int branchId, int orderId, int zoneId, int warehouseId);
    }
}
