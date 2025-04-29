using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.DTOs.AdminDTOs;
using Services.DTOs.UserMenuDTOs;
using Services.Models;

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
                .Where(p => p.LocalId == localId && p.IsDeleted == false)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    AdditionalDescription = p.AdditionalDescription,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    HaveImage = p.HaveImage,
                    Categories = p.SubcategoryProducts
                        .Select(sp => sp.SubcategoryId)
                        .ToList()
                }).ToListAsync();

            return Ok(products);
        }


        [HttpGet("admin/{adminId}")]
        public async Task<IActionResult> GetProductAdmin(Guid adminId)
        {
            var admin = await _context.Admins
                .Include(a => a.Local)
                .FirstOrDefaultAsync(a => a.Id == adminId);

            if (admin == null)
            {
                return NotFound("Admin not found");
            }

            var allSubcategories = await _context.Subcategories
                .Select(sc => new SubcategoryDto
                {
                    Id = sc.Id,
                    Name = sc.Name
                })
                .ToListAsync();

            var products = await _context.Products
                .Where(p => p.LocalId == admin.LocalId)
                .Select(p => new ProductGetDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    AdditionalDescription = p.AdditionalDescription,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    IsDeleted = p.IsDeleted,
                    SelectedSubCategories = p.SubcategoryProducts.Select(sp => new SubcategoryDto
                    {
                        Id = sp.Subcategory.Id,
                        Name = sp.Subcategory.Name
                    }).ToList(),
                    HaveImage = p.HaveImage
                })
                .ToListAsync();

            foreach (var product in products)
            {
                product.AllSubCategories = allSubcategories;
            }

            return Ok(products);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductUpdateDto dto)
        {
            var product = await _context.Products
                .Include(p => p.SubcategoryProducts) 
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(dto.ImageUrl))
            {
                product.ImageUrl = null;
                product.HaveImage = false;
            }
            else
            {
                product.ImageUrl = dto.ImageUrl;
                product.HaveImage = true;
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.AdditionalDescription = dto.AdditionalDescription;
            product.Price = dto.Price;
            product.IsDeleted = dto.IsDeleted;

            _context.SubcategoryProducts.RemoveRange(product.SubcategoryProducts);

            foreach (var subcategoryId in dto.SelectedSubCategoryIds)
            {
                var subcategoryProduct = new SubcategoryProduct
                {
                    ProductId = product.Id,
                    SubcategoryId = subcategoryId
                };
                _context.SubcategoryProducts.Add(subcategoryProduct);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto dto)
        {
            var admin = await _context.Admins
                .Include(a => a.Local)
                .FirstOrDefaultAsync(a => a.Id == dto.AdminId);

            if (admin == null)
            {
                return NotFound("Admin not found");
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                AdditionalDescription = dto.AdditionalDescription,
                Price = dto.Price,
                ImageUrl = string.IsNullOrEmpty(dto.ImageUrl) ? null : dto.ImageUrl,
                HaveImage = !string.IsNullOrEmpty(dto.ImageUrl),
                IsDeleted = false,
                LocalId = admin.LocalId
            };

            _context.Products.Add(product);

            foreach (var subcategoryId in dto.SelectedSubCategoryIds)
            {
                _context.SubcategoryProducts.Add(new SubcategoryProduct
                {
                    ProductId = product.Id,
                    SubcategoryId = subcategoryId
                });
            }

            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
