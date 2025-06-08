using Services.Aplication.DTOs.Notification;
using System.Threading.Tasks;

namespace Services.Aplication.Interfaces.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<GetNotificationDto>> GetByLocalIdAsync(Guid localId);
        Task CreateAsync(CreateNotificationDto dto, Guid localId);
        Task UpdateAsync(UpdateNotificationDto dto, Guid id);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<GetNotificationDto>> GetByLocalForUsersAsync(Guid localId);

    }
}
