using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.DTOs.UserMenuDTOs;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{localId}")]
        public async Task<IActionResult> GetProducts(Guid localId)
        {

            var local = await _context.Locals
                .FirstOrDefaultAsync(l => l.Id == localId);

            if (local == null)
            {
                return NotFound("Local not found");
            }

            var products = await _context.Products
                .Where(p => p.LocalId == localId)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    AdditionalDescription = p.AdditionalDescription,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Categories = _context.CategoryProducts
                        .Where(cp => cp.ProductsId == p.Id)
                        .Select(cp => cp.Category.Id)
                        .ToList()
                }).ToListAsync();

            return Ok(products);
        }

    }
}
