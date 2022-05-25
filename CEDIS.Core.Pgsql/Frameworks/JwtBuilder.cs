using CEDIS.Core.Pgsql.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CEDIS.Core.Pgsql.Frameworks
{
    public class JwtBuilder
    {
        private readonly IConfiguration iconfiguration;
        public JwtBuilder(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

        public LoginToken Build(ClaimsIdentity claimsIdentity)
        {
            var tokenHanderl = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer= "https://localhost:5001/",
                Audience= "https://localhost:5001/",
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHanderl.CreateToken(tokenDescriptor);
            return new LoginToken { Token = tokenHanderl.WriteToken(token)};
        }
    }
}
