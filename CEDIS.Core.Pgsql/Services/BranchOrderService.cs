using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using CEDIS.Core.Pgsql.Services.Interfaces;
using CEDIS.Core.Pgsql.Persistences;
using CEDIS.Core.Pgsql.DTOs;
using CEDIS.Core.Pgsql.Frameworks.Helpers;
using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Enums;

namespace CEDIS.Core.Pgsql.Services
{
    public class BranchOrderService : IBranchOrderService
    {

        private readonly int ENTITY_ID_INCREMENT = 1;
        private readonly int ZERO_INCREMENT_ON_REQUEST = 0;

        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public BranchOrderService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<BranchOrderViewDto>> Get()
        {
            var result = await _dbContext.BranchOrder
                .Include(x => x.Status)
                .Include(x => x.Branch)
                    .ThenInclude(x => x.Schedule)
                .Include(x => x.Warehouse)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BranchOrderViewDto>>(result);
        }

        public async Task<IEnumerable<BranchOrderViewDto>> GetByWarehouse(int warehouseId)
        {
            var result = await _dbContext.BranchOrder
                .Include(x => x.Status)
                .Include(x => x.Branch)
                    .ThenInclude(x => x.Schedule)
                .Include(x => x.Warehouse)
                .Where(x=>x.WarehouseId == warehouseId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BranchOrderViewDto>>(result);
        }

        public async Task<IEnumerable<BranchOrderViewDto>> GetAllByBranch(int branchId)
        {
            var result = await _dbContext.BranchOrder
                .Include(x => x.Status)
                .Include(x => x.Branch)
                    .ThenInclude(x => x.Schedule)
                .Include(x => x.Warehouse)
                .Where(x=>x.BranchId == branchId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BranchOrderViewDto>>(result);
        }

        public async Task<BranchOrderViewDto> GetById(int id, int branchId)
        {
            var result = await _dbContext.BranchOrder
                .Include(x => x.Branch)
                    .ThenInclude(x=>x.Schedule)
                .Include(x => x.Warehouse)
                .Include(x => x.Status)
                .Include(x => x.BranchOrderDetails)
                        .ThenInclude(x => x.PresentationWarehouse)
                            .ThenInclude(x => x.Presentation)
                .Include(x => x.BranchOrderDetails)
                        .ThenInclude(x => x.PresentationWarehouse)
                            .ThenInclude(x => x.Zones)
                .Include(x => x.BranchOrderDetails)
                    .ThenInclude(x => x.Unit)
                .Where(x => x.BranchId == branchId && x.OrderId == id).FirstOrDefaultAsync();
            return _mapper.Map<BranchOrderViewDto>(result);
        }

        public async Task<BranchOrderHeaderViewDto> GetOrderHeaderById(int id, int branchId, int zoneId)
        {
            var result = await _dbContext.Orders
                 .Include(x => x.Branch)
                 .Include(x => x.Status)
                 .Include(x => x.Mode)
                 .Include(x => x.OrderDetails)
                        .ThenInclude(x => x.PresentationWarehouse)
                            .ThenInclude(x => x.Zones)
                .Include(x => x.OrderDetails)
                        .ThenInclude(x => x.PresentationWarehouse)
                            .ThenInclude(x => x.Presentation)
                 .Include(x => x.OrderDetails)
                     .ThenInclude(x => x.Units)
                 .Include(x => x.OrderDetails)
                     .ThenInclude(x => x.Status)
                 .Where(x => x.BranchId == branchId && x.OrderId == id && x.ZoneId == zoneId).FirstOrDefaultAsync();

            return _mapper.Map<BranchOrderHeaderViewDto>(result);
        }

        public async Task<BranchOrderViewDto> AddAllAsync(int branchId, BranchOrderCreate orders)
        {
            try
            {
                var currentId = IdentityIncrement<BranchOrder>.GetNextId(_dbContext, nameof(BranchOrder.BranchId), branchId.ToString(), nameof(BranchOrder.OrderId), ZERO_INCREMENT_ON_REQUEST);
                var newOrders = _mapper.Map<BranchOrderCreate, BranchOrder>(orders, opts =>
                {
                    opts.AfterMap((src, dest) =>
                    {
                        currentId += ENTITY_ID_INCREMENT;
                        dest.OrderId = currentId;
                        dest.BranchId = branchId;
                        dest.StatusId = Enums.StatusEnum.Send;
                        foreach (var detail in dest.BranchOrderDetails)
                        {
                            detail.WarehouseId = dest.WarehouseId;
                            detail.BranchId = branchId;
                        }
                    });
                });

                var results = _dbContext.BranchOrder.Add(newOrders);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    var save = await _dbContext.BranchOrder
                        .Include(x => x.Status)
                        .Include(x => x.Branch)
                            .ThenInclude(x=>x.Schedule)
                        .Include(x => x.Warehouse)
                        .Where(x => x.BranchId == results.Entity.BranchId && x.OrderId == results.Entity.OrderId)
                        .FirstOrDefaultAsync();
                    return _mapper.Map<BranchOrderViewDto>(save);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<BranchOrderViewDto> Publish(int branchId, int orderId, bool divider = false)
        {
            try
            {
                var header = await _dbContext
                    .BranchOrder
                    .Include(x=>x.Branch).ThenInclude(x=>x.Schedule)
                    .Include(x => x.BranchOrderDetails)
                            .ThenInclude(x => x.PresentationWarehouse)
                                .ThenInclude(x => x.Presentation)
                    .Where(x => x.BranchId == branchId && x.OrderId == orderId && x.StatusId == Enums.StatusEnum.Send)
                    .FirstOrDefaultAsync();

                if (header == null)
                    return null;

                header.OrderHeaders = new List<OrderHeader>();

                if (divider)
                {
                    var zones = header.BranchOrderDetails
                        .Select(x =>
                        new OrderDetail
                        {
                            WarehouseId = x.WarehouseId,
                            BranchId = x.BranchId,
                            OrderId = x.OrderId,
                            ZoneId = x.PresentationWarehouse.ZoneId,
                            PresentationId = x.PresentationId,
                            ProductId = x.ProductId,
                            StatusId = StatusDetailEnum.PendingProcess,
                            Cost = x.Cost,
                            UnitId = x.UnitId,
                            QuantityAvailable = x.OrderedQuantity
                        }).GroupBy(x => x.ZoneId).OrderBy(x => x.Key)
                        .ToList();
                    foreach (var item in zones)
                    {
                        header.OrderHeaders.Add(new OrderHeader
                        {
                            WarehouseId = header.WarehouseId,
                            BranchId = header.BranchId,
                            OrderId = header.OrderId,
                            ZoneId = item.Key,
                            StatusId = StatusEnum.Managed,
                            ModeId = _dbContext.Modes.FirstOrDefault(x => x.Abrebiature == Convert.ToChar(header.Mode)).Id,
                            OrderDetails = item.ToList()
                        });
                    }
                }
                else
                {
                    var detail = header.BranchOrderDetails
                        .Select(x =>
                        new OrderDetail
                        {
                            WarehouseId = x.WarehouseId,
                            BranchId = x.BranchId,
                            OrderId = x.OrderId,
                            ZoneId = 99,
                            PresentationId = x.PresentationId,
                            ProductId = x.ProductId,
                            StatusId = StatusDetailEnum.PendingProcess,
                            Cost = x.Cost,
                            UnitId = x.UnitId,
                            QuantityAvailable = x.OrderedQuantity
                        }).OrderBy(x => x.ZoneId)
                        .ToList();

                    header.OrderHeaders.Add(new OrderHeader
                    {
                        WarehouseId = header.WarehouseId,
                        BranchId = header.BranchId,
                        OrderDate = header.Date,
                        OrderId = header.OrderId,
                        ZoneId = 99,
                        StatusId = Enums.StatusEnum.Managed,
                        ModeId = _dbContext.Modes.FirstOrDefault(x => x.Abrebiature == Convert.ToChar(header.Mode)).Id,
                        OrderDetails = detail
                    });
                }
                header.StatusId = Enums.StatusEnum.Start;

                _dbContext.Update(header);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    var save = await _dbContext.BranchOrder
                        .Include(x => x.Status)
                        .Include(x => x.Branch)
                        .Include(x => x.Warehouse)
                        .Where(x => x.BranchId == branchId && x.OrderId == orderId)
                        .FirstOrDefaultAsync();
                    return _mapper.Map<BranchOrderViewDto>(save);
                }
                else
                    return null;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ChangeCenter(ChangeCenterDTO update)
        {
            try
            {
                var result = await _dbContext.BranchOrder
                        .Include(x => x.BranchOrderDetails)
                        .FirstOrDefaultAsync(x => x.BranchId == update.BranchId && x.OrderId == update.OrderId);

                _dbContext.BranchOrder.Remove(result);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    result.BranchOrderDetails.ForEach(x => x.WarehouseId = update.WarehouseId);
                    result.WarehouseId = update.WarehouseId;
                    _dbContext.BranchOrder.Add(result);
                    return await _dbContext.SaveChangesAsync() > 0;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public async Task<List<BranchOrderHeaderViewDto>> GetHeaders()
        {
            var result = await _dbContext.Orders
                .Include(x => x.Branch)
                .Include(x => x.Status)
                .Include(x => x.Mode)
                .Include(x => x.Zones)
                .ToListAsync();

            return _mapper.Map<List<BranchOrderHeaderViewDto>>(result);
        }
    }
}
