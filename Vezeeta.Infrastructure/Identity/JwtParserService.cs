using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Interfaces;
using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Infrastructure.Identity
{
    public class JwtParserService : IJwtParserService
    {
        private readonly IConfiguration _configuration;

        public JwtParserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtClaims ParseJwt(string token)
        {
            // Get the secret key from configuration (this should be securely stored)

            var tokenHandler = new JwtSecurityTokenHandler();

            var userId = (tokenHandler.ReadToken(token) as JwtSecurityToken).Subject;

            return new JwtClaims
            {
                UserId = int.Parse(userId)
            };
        }
    }
}
