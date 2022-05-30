using CEDIS.Core.Pgsql.Enums;
using CEDIS.Core.Pgsql.Frameworks;
using CEDIS.Core.Pgsql.Models;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Picking.Core.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CEDIS.Picking.API.Pgsql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService AuthService;
        private readonly JwtBuilder JwtBuilder;

        public AuthController(IAuthService authService, JwtBuilder JwtBuilder)
        {
            AuthService = authService;
            this.JwtBuilder = JwtBuilder;
        }

        [AllowAnonymous]
        [HttpPost("user/authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Authenticate([FromBody] Credential credential)
        {
            var user = await AuthService.Authenticate(credential);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            if(user.TwoFactorAuthenticator != null)
            {
                return Ok(new { twoFactorNeed=true });
            }

            var claims = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Id.ToString()),
                new Claim(CustomClaimsType.LogInType, UserLogInType.User.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            var token = JwtBuilder.Build(claims);

            var authUser = new { result = user, token = token.Token, type = UserLogInType.User.ToString() };

            return Ok(authUser);
        }

        [Authorize]
        [HttpGet("qr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult GetQr()
        {
            return Ok(AuthService.GetSecretToQr());
        }

        [Authorize]
        [HttpPost("qr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateQr(int userId,TwoFactorAuthenticator twoFactor,string code)
        {
            return Ok(await AuthService.User2FACreate(userId,twoFactor,code));
        }

        [AllowAnonymous]
        [HttpPost("user/2fa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Authenticate(string code, int userId)
        {
            if (await AuthService.User2FAValid(userId, code))
                return BadRequest(new { message = "Codigo Incorrecto." });

            var claims = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name,userId.ToString()),
                new Claim(CustomClaimsType.LogInType, UserLogInType.User.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            var token = JwtBuilder.Build(claims);

            var authUser = new { result = userId, token = token.Token, type = UserLogInType.User.ToString() };

            return Ok(authUser);
        }


        [AllowAnonymous]
        [HttpPost("branch/authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Authenticate([FromBody] BranchCredential branchCredential)
        {
            var authBranch = await AuthService.Authenticate(branchCredential);

            if (authBranch == null)
                return BadRequest(new { message = "Something went wrong on authentication." });

            var claims = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name,authBranch.Id.ToString()),
                new Claim(CustomClaimsType.LogInType, UserLogInType.Branch.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            var token = JwtBuilder.Build(claims);
            var authUser = new { result=authBranch, token=token.Token, type=UserLogInType.Branch.ToString() };

            return Ok(authUser);
        }

        [AllowAnonymous]
        [HttpPost("warehouse/authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] WarehouseCredential clientCredential)
        {
            var result = await AuthService.Authenticate(clientCredential);

            if (result==null)
                throw new Exception("Company or Api Key doesn't exist.");

            var claims = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name,result.Id.ToString()),
                new Claim(CustomClaimsType.LogInType, UserLogInType.Warehouse.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            var token = JwtBuilder.Build(claims);

            var authUser = new { result = result, token = token.Token, type = UserLogInType.Warehouse.ToString() };

            return Ok(authUser);
        }
    }
}
