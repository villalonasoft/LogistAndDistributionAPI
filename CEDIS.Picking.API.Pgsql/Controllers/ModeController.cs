using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Persistences;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEDIS.Picking.API.Pgsql.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ModeController : ControllerBase
    {
        private readonly IModeService _unit;

        public ModeController(IModeService unit)
        {
            _unit = unit;
        }

        // GET: api/<UnitController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unit.GetResponseAsync());
        }

        // POST api/<UnitController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Mode units)
        {
            return Ok(await _unit.Create(units));
        }

        // PUT api/<UnitController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody] Mode units)
        {
            return Ok(await _unit.Update(id,units));
        }

        // DELETE api/<UnitController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
