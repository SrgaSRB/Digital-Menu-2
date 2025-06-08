using Services.Aplication.DTOs.Notification;
using Services.Domain.Models;

namespace Services.Aplication.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<IReadOnlyList<Notification>> GetListByLocalIdAsync(Guid localId);
        Task<Notification?> GetAsync(Guid id);
        Task SaveAsync();
        Task DeleteAsync(Guid id);
        Task AddAsync(Notification notification);

    }
}
