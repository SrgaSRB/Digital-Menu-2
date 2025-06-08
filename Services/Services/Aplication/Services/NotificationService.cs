using Services.Aplication.DTOs.Notification;
using Services.Aplication.Interfaces.Repositories;
using Services.Aplication.Interfaces.Services;
using Services.Domain.Models;

namespace Services.Aplication.Services
{
    public class NotificationService : INotificationService
    {

        private readonly INotificationRepository _notifyRepo;

        public NotificationService(INotificationRepository notifyRepo) => _notifyRepo = notifyRepo;

        public async Task CreateAsync(CreateNotificationDto dto, Guid localId)
        {
            Notification newNotification = new Notification
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LocalId = localId,
                Message = dto.Text,
                Title = dto.Title,
            };

            await _notifyRepo.AddAsync(newNotification);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _notifyRepo.DeleteAsync(id);
            await _notifyRepo.SaveAsync();
        }

        public async Task<IEnumerable<GetNotificationDto>> GetByLocalForUsersAsync(Guid localId)
        {
            var notifications = await _notifyRepo.GetListByLocalIdAsync(localId)
                ?? throw new Aplication.Exceptions.NotFoundException($"Notifications for local with id: {localId} not found");

            return notifications.Select(x => new GetNotificationDto { Id = x.Id, Text = x.Message, Title = x.Title }).ToList();
        }

        public async Task<IEnumerable<GetNotificationDto>> GetByLocalIdAsync(Guid localId)
        {
            var notifications = await _notifyRepo.GetListByLocalIdAsync(localId)
                ?? throw new Aplication.Exceptions.NotFoundException($"Notifications for local with id: {localId} not found");

            return notifications.Select(x => new GetNotificationDto { Id = x.Id, Text = x.Message, Title = x.Title }).ToList();
        }

        public async Task UpdateAsync(UpdateNotificationDto dto, Guid id)
        {
            var notification = await _notifyRepo.GetAsync(id)
                ?? throw new Aplication.Exceptions.NotFoundException($"Notification with id: {id} not found");

            notification.Title = dto.Title;
            notification.Message = dto.Text;

            await _notifyRepo.SaveAsync();

        }
    }
}
