using Services.Aplication.DTOs.Auth;
using Services.Aplication.Interfaces.Repositories;
using Services.Aplication.Interfaces.Security;
using Services.Aplication.Interfaces.Services;
using Services.Domain.Models;

namespace Services.Aplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IJwtGenerator _jwt;

        public AuthService(IAdminRepository adminRepository, IJwtGenerator jwt)
        {
            _adminRepository = adminRepository;
            _jwt = jwt;
        }

        public async Task<TokenDto> LoginUserAsync(LoginDto dto, CancellationToken ct)
        {
            //login validator

            Admin admin = await _adminRepository.GetAdminByUsernameAsync(dto.Username, ct);

            if (admin == null)
                throw new Exceptions.UnauthorizedAccessException("Invalid credentials");

            //if (!BCrypt.Net.BCrypt.Verify(dto.Password, admin.PasswordHash))
            if (admin.PasswordHash != dto.Password)
                throw new Exceptions.UnauthorizedAccessException("Invalid credentials");

            return _jwt.GenerateToken(admin);
        }
    }
}
