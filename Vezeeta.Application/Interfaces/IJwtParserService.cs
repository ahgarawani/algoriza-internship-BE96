using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IJwtParserService
    {
        JwtClaims ParseJwt(string token);
    }
}
