using Services.Aplication.DTOs.Auth;

namespace Services.Aplication.Interfaces.Services
{
    public interface IAuthService
    {
        Task<TokenDto> LoginUserAsync(LoginDto dto, CancellationToken ct);
    }
}
