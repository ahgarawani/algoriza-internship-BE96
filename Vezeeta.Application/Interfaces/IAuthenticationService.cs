using Vezeeta.Application.Mappings.DTOs;

namespace Vezeeta.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<AuthenticationResponse> LoginAsync(LoginRequest loginRequest);
    }
}