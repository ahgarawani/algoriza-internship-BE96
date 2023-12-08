using Microsoft.AspNetCore.Identity;

namespace Vezeeta.Infrastructure.Identity
{
    public interface IAuthManager
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<AuthResponse> LoginAsync(LoginRequest model);
    }
}