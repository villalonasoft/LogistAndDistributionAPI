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
    public class PickingService : IPickingService
    {
        private readonly ApplicationDbContext _dbContext;

        public PickingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //asignamet order to user, fisrt order on date desc
        public async Task<OrderHeader> RandomOrder(int userId, AssingOrderPost assingOrder)
        {
            try
            {
                var order = await _dbContext.Orders
                    .Where(x =>
                        x.UserId == null &&
                        x.StatusId == Enums.StatusEnum.Start &&
                        x.ZoneId == assingOrder.ZoneId &&
                        x.WarehouseId == assingOrder.WarehouseId &&
                        x.ModeId == assingOrder.ModeId)
                    .OrderByDescending(x => x.OrderDate)
                    .FirstOrDefaultAsync();

                order.UserId = userId;
                order.DateInit = DateTime.Now;
                order.StatusId = Enums.StatusEnum.Asigned;

                await _dbContext.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //asignament spesific order to user
        public async Task<OrderHeader> PickOrder(int branchId, int orderId, int zoneId, int userId, int warehouseId)
        {
            try
            {
                var order = await _dbContext.Orders
                    .Where(x =>
                        x.UserId == null &&
                        x.StatusId == Enums.StatusEnum.Start &&
                        x.ZoneId == zoneId &&
                        x.OrderId == orderId &&
                        x.BranchId == branchId &&
                        x.WarehouseId == warehouseId)
                    .OrderBy(x => x.OrderDate)
                    .FirstOrDefaultAsync();

                order.UserId = userId;
                order.DateInit = DateTime.Now;
                order.StatusId = Enums.StatusEnum.Asigned;

                await _dbContext.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //list of order to user
        public async Task<IEnumerable<OrderHeader>> MyOrders(int UserId)
        {
            return await _dbContext.Orders.Where(x => x.UserId == UserId && x.StatusId == Enums.StatusEnum.Asigned).ToListAsync();
        }

        //get detail from order
        public async Task<OrderHeader> GetOrderDetail(int branchId, int orderId, int zoneId, int warehouseId)
        {
            return await _dbContext.Orders
                    .Include(x => x.OrderDetails)
                    .Where(x =>
                        x.StatusId >= Enums.StatusEnum.Asigned && x.StatusId < Enums.StatusEnum.EndPicking &&
                        x.ZoneId == zoneId &&
                        x.OrderId == orderId &&
                        x.BranchId == branchId &&
                        x.WarehouseId == warehouseId)
                    .FirstOrDefaultAsync();
        }
    }
}
