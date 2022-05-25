using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CEDIS.Core.Pgsql.DTOs;
using CEDIS.Core.Pgsql.HubConfig;
using Microsoft.AspNetCore.SignalR;
using CEDIS.Core.Pgsql.Frameworks.Extention;
using Microsoft.AspNetCore.Authorization;

namespace CEDIS.Picking.API.Pgsql.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/orders")]
    public class BranchOrderController : Controller
    {
        private readonly IHubContext<BranchOrderHub> _hub;
        private readonly IHubContext<HeadersHub> _hubHeader;

        private readonly IBranchOrderService _branchOrder;
        private readonly IBranchServices _branch;

        public BranchOrderController(
            IBranchOrderService branchOrder,
            IBranchServices branch,
            IHubContext<BranchOrderHub> hub,
            IHubContext<HeadersHub> hubHeader)
        {
            _branchOrder = branchOrder;
            _branch = branch;

            _hub = hub;
            _hubHeader = hubHeader;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var branchContext = HttpContext.User.GetLogInType();

            if (branchContext == Core.Pgsql.Enums.UserLogInType.Branch)
            {
                var branch = HttpContext.User.GetAuthorizedBranch();
                var newOrder = await _branchOrder.GetAllByBranch(branch.Id);
                if (newOrder != null)
                    return Ok(newOrder);
                return StatusCode(204);
            }
            else if (branchContext == Core.Pgsql.Enums.UserLogInType.Warehouse)
            {
                var warehouse = HttpContext.User.GetAuthorizedWarehouse();
                var newOrder = await _branchOrder.GetByWarehouse(warehouse.Id);
                if (newOrder != null)
                    return Ok(newOrder);
                return StatusCode(204);
            }
            else
            {
                var newOrder = await _branchOrder.Get();
                if (newOrder != null)
                    return Ok(newOrder);
                return StatusCode(204);
            }
        }

        [HttpGet("/api/headers")]
        public async Task<IActionResult> GetHeaders()
        {
            var newOrder = await _branchOrder.GetHeaders();
            return Ok(newOrder);
        }

        [HttpGet("{id}/branch/{branchId}")]
        public async Task<IActionResult> GetByIdAsync(int id, int branchId)
        {
            var newOrder = await _branchOrder.GetById(id, branchId);
            return Ok(newOrder);
        }

        [HttpGet("{id}/branch/{branchId}/zone/{zoneId}")]
        public async Task<IActionResult> GetHeaderByIdAsync(int id, int branchId, int zoneId)
        {
            var newOrder = await _branchOrder.GetOrderHeaderById(id, branchId, zoneId);

            return Ok(newOrder);
        }

        [HttpPost("{id}/branch/{branchId}")]
        public async Task<IActionResult> Publish(int id, int branchId, [FromQuery] bool divider)
        {
            var newOrder = await _branchOrder.Publish(branchId, id, divider);
            if (newOrder != null)
            {
                await _hub.Clients.All.SendAsync("ListOrders", newOrder);
                await _hubHeader.Clients.All.SendAsync("ListHeaders", await _branchOrder.GetHeaders());
                return StatusCode(201);
            }
            return StatusCode(401);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PostAsync([FromQuery] string apikey, [FromBody] BranchOrderCreate orders)
        {
            var branch = await _branch.GetBranchId(apikey);
            try
            {
                var newOrder = await _branchOrder.AddAllAsync(branch.Id, orders);
                if (newOrder != null)
                    await _hub.Clients.All.SendAsync("ListOrders", newOrder);
                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("changecenter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ChangeCenter(ChangeCenterDTO change)
        {
            try
            {
                var update = await _branchOrder.ChangeCenter(change);
                return Ok(update);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
