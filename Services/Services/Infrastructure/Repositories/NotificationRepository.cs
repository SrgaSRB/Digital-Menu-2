using Microsoft.EntityFrameworkCore;
using Services.Aplication.DTOs.Notification;
using Services.Aplication.Interfaces.Repositories;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Notifications
                .Where(n => n.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Notification?> GetAsync(Guid id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IReadOnlyList<Notification>> GetListByLocalIdAsync(Guid localId)
        {
            return await _context.Notifications
                .Where(n => n.LocalId == localId)
                .ToListAsync();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

    }
}
