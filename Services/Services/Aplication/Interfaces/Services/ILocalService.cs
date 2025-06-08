using Services.Aplication.DTOs.Local;
using Services.Aplication.DTOs.UserMenuDTOs;

namespace Services.Aplication.Interfaces.Services
{
    public interface ILocalService
    {
        Task<GetLocalHeaderDto> GetHeaderAsync(Guid localId);
        Task<GetLocalInfoDto> GetInfoAsync(Guid localId);
        Task UpdateAsync(UpdateLocalDto dto, Guid localId);
    }
}
