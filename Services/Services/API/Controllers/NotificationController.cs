using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Aplication.DTOs.AdminDTOs;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private readonly AppDbContext _context;

        public NotificationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{localId}")]
        public async Task<IActionResult> GetNotifications(Guid localId)
        {

            var local = await _context.Locals
                .FindAsync(localId);

            if (local == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications
                .Where(n => n.LocalId == localId)
                .Select(n => new NotificationGetDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Text = n.Message
                }).ToListAsync();

            return Ok(notifications);

        }

        [HttpPost("{localId}")]
        public async Task<IActionResult> CreateNotification(Guid localId, [FromBody] NotificationCreateDto dto)
        {

            var local = await _context.Locals
                .FindAsync(localId);

            if (local == null)
            {
                return NotFound();
            }

            var newNotification = new Notification
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Message = dto.Text,
                LocalId = localId,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Notifications.Add(newNotification);

            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(Guid id, [FromBody] NotificationCreateDto dto)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            notification.Title = dto.Title;
            notification.Message = dto.Text;

            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("by-local/{localId}")]
        public async Task<IActionResult> GetNotificationForUSer(Guid localId)
        {

            var local = await _context.Locals
                .FindAsync(localId);

            if (local == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications
                .Where(n=> n.LocalId == localId)
                .Select(n => new NotificationGetDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Text = n.Message
                }).ToListAsync();

            return Ok(notifications);

        }

    }
}
