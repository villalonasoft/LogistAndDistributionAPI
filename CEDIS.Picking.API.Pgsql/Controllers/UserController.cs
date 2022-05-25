using CEDIS.Core.Pgsql.DTOs;
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
    public class UserController : ControllerBase
    {
        private readonly IUserSevice _userservices;

        public UserController(IUserSevice userservices)
        {
            _userservices = userservices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _userservices.GetAll());

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int userId) => Ok(await _userservices.GetById(userId));

        [HttpPost]
        public async Task<IActionResult> AddUser(UserCreateDto user) => Ok(await _userservices.AddAsync(user));

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, UserCreateDto user) => Ok(await _userservices.UpdateAsync(userId,user));
    }
}
