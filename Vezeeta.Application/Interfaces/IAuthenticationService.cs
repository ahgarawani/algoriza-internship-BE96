using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseDTO> RegisterAsync(RegisterRequestDTO registerRequest);
        Task<AuthenticationResponseDTO> LoginAsync(LoginRequestDTO loginRequest);
    }
}