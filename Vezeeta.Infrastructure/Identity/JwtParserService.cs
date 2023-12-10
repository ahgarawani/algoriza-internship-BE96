using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
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
            var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var userId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;
            var userRole = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "roles")?.Value;

            return new JwtClaims
            {
                UserId = int.Parse(userId),
                UserRole = userRole
            };
        }
    }
}
