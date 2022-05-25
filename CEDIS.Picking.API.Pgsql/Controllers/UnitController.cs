using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Persistences;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEDIS.Picking.API.Pgsql.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unit;

        public UnitController(IUnitService unit)
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
        public async Task<IActionResult> Post([FromBody]Units units)
        {
            return Ok(await _unit.Create(units));
        }

        // PUT api/<UnitController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody] Units units)
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
