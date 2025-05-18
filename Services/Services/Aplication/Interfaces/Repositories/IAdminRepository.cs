using Services.Domain.Models;

namespace Services.Aplication.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        Task<Admin> GetAdminByUsernameAsync(string username, CancellationToken ct);
    }
}
