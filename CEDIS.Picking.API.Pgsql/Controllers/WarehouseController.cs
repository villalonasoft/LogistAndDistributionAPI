using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CEDIS.Picking.API.Pgsql.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetWarehouse()
        {
            return Ok(await _warehouseService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _warehouseService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Warehouse warehouse)
        {
            var result = await _warehouseService.Create(warehouse);
            if (result.Data != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Warehouse warehouse)
        {
            var result = await _warehouseService.Update(id, warehouse);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
