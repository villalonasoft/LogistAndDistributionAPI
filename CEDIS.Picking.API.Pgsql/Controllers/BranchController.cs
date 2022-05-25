using CEDIS.Core.Pgsql.DTOs;
using CEDIS.Core.Pgsql.HubConfig;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CEDIS.Picking.API.Pgsql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchController : ControllerBase
    {
        private IHubContext<HeadersHub> _hub;
        private readonly IBranchServices branchServices;

        public BranchController(IBranchServices branchServices, IHubContext<HeadersHub> hub)
        {
            this.branchServices = branchServices;
            _hub = hub;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWarehouse()
        {
            return Ok(await branchServices.GetWarehouse());
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await branchServices.GetBranches());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await branchServices.GetBranchById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(BranchViewDto dto)
        {
            try
            {
                return Ok(await branchServices.CreateBranch(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, BranchViewDto dto)
        {
            try
            {
                return Ok(await branchServices.UpdateBranch(id, dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
