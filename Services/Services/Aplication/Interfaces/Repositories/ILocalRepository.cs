using Services.Aplication.DTOs.Local;
using Services.Aplication.DTOs.UserMenuDTOs;
using Services.Domain.Models;

namespace Services.Aplication.Interfaces.Repositories
{
    public interface ILocalRepository
    {
        Task<Local?> GetAsync(Guid id);
        Task<GetLocalHeaderDto?> GetHeaderAsync(Guid id);
        Task SaveAsync();

    }
}
