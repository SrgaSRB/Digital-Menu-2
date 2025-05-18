using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Aplication.DTOs.AdminDTOs;
using Services.Aplication.DTOs.UserMenuDTOs;
using Services.Infrastructure.Data;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : ControllerBase
    {

        private readonly AppDbContext _context;

        public LocalController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("header/{localId}")]
        public async Task<IActionResult> GetLocalHeader(Guid localId)
        {
            var local = await _context.Locals.FindAsync(localId);
            if (local == null)
            {
                return NotFound();
            }

            var localHeader = await _context.Locals
                .Where(l => l.Id == localId)
                .Select(l => new LocalHeaderDto
                {
                    Name = l.Name,
                    ImageUrl = l.LogoUrl
                }).FirstOrDefaultAsync();

            if (localHeader == null)
            {
                return NotFound();
            }

            return Ok(localHeader);
        }

        [HttpGet("{localId}")]
        public async Task<IActionResult> GetLocalInfo(Guid localId)
        {
            var local = await _context.Locals
                .Where(l => l.Id == localId)
                .Select(l => new LocalGetDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    ImageUrl = l.LogoUrl,
                    Subscription = l.Subscription.EndDate
                })
                .FirstOrDefaultAsync();

            if (local == null)
                return NotFound();

            return Ok(local);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocal(Guid id, [FromBody] LocalUpdateDto dto)
        {
            var local = await _context.Locals.FindAsync(id);
            if (local == null)
                return NotFound("Lokal nije pronađen");

            local.Name = dto.Name;

            if (string.IsNullOrEmpty(dto.ImageUrl))
            {
                local.LogoUrl = null;
                local.HaveLogo = false;
            }
            else
            {
                local.LogoUrl = dto.ImageUrl;
                local.HaveLogo = true;
            }

            await _context.SaveChangesAsync();
            return Ok("Lokal uspešno ažuriran");
        }




    }
}
