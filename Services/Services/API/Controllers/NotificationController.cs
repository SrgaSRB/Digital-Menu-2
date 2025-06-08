using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Aplication.DTOs.AdminDTOs;
using Services.Aplication.DTOs.Notification;
using Services.Aplication.Interfaces.Services;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private readonly INotificationService _notifyService;

        public NotificationController(INotificationService notifyService) => _notifyService = notifyService;

        [HttpGet("{localId}")]
        public async Task<IActionResult> GetNotifications(Guid localId)
        {
            var notifications = await _notifyService.GetByLocalIdAsync(localId);

            return Ok(notifications);
        }

        [HttpPost("{localId}")]
        public async Task<IActionResult> CreateNotification(Guid localId, [FromBody] CreateNotificationDto dto)
        {
            await _notifyService.CreateAsync(dto, localId);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(Guid id, [FromBody] UpdateNotificationDto dto)
        {
            await _notifyService.UpdateAsync(dto, id);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            await _notifyService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("by-local/{localId}")]
        public async Task<IActionResult> GetNotificationForUSer(Guid localId)
        {
            var notifications = await _notifyService.GetByLocalForUsersAsync(localId);

            return Ok(notifications);
        }

    }
}
