using CEDIS.Core.Pgsql.Models;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CEDIS.Picking.API.Pgsql.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PickingController : ControllerBase
    {
        private readonly IPickingService _pickingService;

        public PickingController(IPickingService pickingService)
        {
            _pickingService = pickingService;
        }

        [HttpPost("Assign")]
        public async Task<IActionResult> AssignOrder(AssingOrderPost assingOrder)
        {
            int userId=0;
            _ = await _pickingService.RandomOrder(userId,assingOrder);
            return Ok();
        }
    }
}
