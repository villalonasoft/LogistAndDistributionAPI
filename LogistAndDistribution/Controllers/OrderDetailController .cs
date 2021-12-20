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
    [Route("api/order/{id}/details")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailSimpleVIewDto>>> GetOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetail
                    .Include(x => x.Stock)
                        .ThenInclude(x => x.Presentation)
                            .ThenInclude(x => x.Unit)
                    .Include(x => x.Stock)
                        .ThenInclude(x => x.Presentation)
                            .ThenInclude(x => x.Presentation)
                                .ThenInclude(x => x.Product)
                    .Include(x => x.Stock)
                        .ThenInclude(x => x.Zone)
                .Where(x => x.OrderHeaderId == id && x.CompanyId == 1)
                .ToListAsync();

            var detail = new List<OrderDetailSimpleVIewDto>();


            foreach (var items in orderDetail)
            {
                detail.Add(new OrderDetailSimpleVIewDto
                {
                    Name = items.Stock.Presentation.Presentation.Product.Name + " " + items.Stock.Presentation.Presentation.Name,
                    Stock = items.Stock.Cant,
                    CuantityOrder = items.CuantityOrder,
                    CuantityPicked = items.CuantityPicked,
                    PresentationId = items.PresentationId,
                    ProductId = items.ProductId,
                    Unit = items.Stock.Presentation.Unit.Name,
                    Zone = items.Stock.Zone.Description

                });
            }

            return detail;
        }
    }
}
