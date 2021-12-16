using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogistAndDistribution.Models.Domain;
using LogistAndDitribution.Core.Persistence;
using LogistAndDitribution.Core.Dto.OrdersDTO;

namespace LogistAndDistribution.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetail()
        {
            return await _context.OrderDetail.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderSimpleViewDto>> GetOrderDetail(int id)
        {
            var orderDetail = await _context.OrderHeaders.Include(x => x.Customer)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Stock)
                        .ThenInclude(x => x.Presentation)
                            .ThenInclude(x => x.Unit)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Stock)
                        .ThenInclude(x=>x.Zone)
                .FirstOrDefaultAsync(x=>x.Id == id && x.CompanyId==1);

            var detail = new List<OrderDetailSimpleVIewDto>();


            foreach(var items in orderDetail.OrderDetails)
            {
                detail.Add(new OrderDetailSimpleVIewDto 
                {
                    Stock = items.Stock.Cant,
                    CuantityOrder = items.CuantityOrder,
                    CuantityPicked = items.CuantityPicked,
                    PresentationId = items.PresentationId,
                    ProductId = items.ProductId,
                    Unit = items.Stock.Presentation.Unit.Name,
                    Zone = items.Stock.Zone.Description

                });
            }

            var order = new OrderSimpleViewDto() 
            {
                Customer = orderDetail.Customer.LargeName,
                Status = orderDetail.Status,
                Date = orderDetail.Date,
                EndDate = orderDetail.EndDate,
                Id = orderDetail.Id,
                InitDate = orderDetail.InitDate,
                Mount= orderDetail.Mount,
                Priority = orderDetail.Priority,
                Reference = orderDetail.Reference,
                OrderDetails = detail
            };

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.ZoneId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderHeaderExists(id,1))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(List<OrderPostDto> orderDetail)
        {
            var detail = new List<OrderDetail>();

            foreach(var items in orderDetail)
            {
                var stock = await _context.Stocks.AsNoTracking()
                    .Where(x => x.CompanyId == 1 &&
                        x.PresentationId == items.PresentationId &&
                        x.ProductId == items.ProductId &&
                        x.UnitId == items.UnitId)
                    .Where(x=>x.Cant>0).FirstOrDefaultAsync();

                if (stock != null)
                {
                    detail.Add(new OrderDetail 
                    {
                        CompanyId = 1,
                        CuantityOrder = items.CuantityOrder,
                        OrderHeaderId =1,
                        PresentationId = items.PresentationId,
                        ProductId = items.ProductId,
                        UnitId= items.UnitId,
                        ZoneId = stock.ZoneId
                    });
                }
            }

            var header = new OrderHeader()
            {
                CompanyId = 1,
                Date = DateTime.Now,
                Status = 100,
                Id = 1,
                Mount = 20000,
                OrderTypeId = 1,
                PersonId = 1,
                PersonTypeId = 1,
                Priority = 5,
                Reference = "Embalaje en una sola caja",
                OrderDetails = detail
            };

            var orderzone = detail
                .GroupBy(x => new { x.OrderHeaderId, x.ZoneId, x.CompanyId })
                .Select(x => new OrderZoneUser
                {

                    CompanyId = x.Select(x => x.CompanyId).FirstOrDefault(),
                    OrderHeaderId = x.Select(x => x.OrderHeaderId).FirstOrDefault(),
                    ZoneId = x.Select(x => x.ZoneId).FirstOrDefault()

                }).ToList();

            _context.OrderHeaders.Add(header);
            _context.OrderZoneUsers.AddRange(orderzone);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderHeaderExists(1,1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderDetail", new { id = header.Id }, orderDetail);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderDetail>> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetail.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetail.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return orderDetail;
        }

        private bool OrderHeaderExists(int companyId,int id)
        {
            return _context.OrderHeaders.Any(e => e.CompanyId ==companyId && e.Id==id);
        }
    }
}
