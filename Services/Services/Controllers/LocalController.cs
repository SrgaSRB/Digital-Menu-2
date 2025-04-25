using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.DTOs.UserMenuDTOs;

namespace Services.Controllers
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


    }
}
