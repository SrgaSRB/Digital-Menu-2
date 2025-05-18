using Services.Aplication.DTOs.Auth;
using Services.Domain.Models;

namespace Services.Aplication.Interfaces.Security
{
    public interface IJwtGenerator
    {
        TokenDto GenerateToken(Admin admin);
    }
}
