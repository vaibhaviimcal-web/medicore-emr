using MediCore.Application.DTOs.Auth;

namespace MediCore.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<bool> UserExistsAsync(string email);
}
