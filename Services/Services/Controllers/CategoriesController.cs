using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.DTOs.AdminDTOs;
using Services.Models;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{localId}")]
        public async Task<IActionResult> GetCategories(Guid localId)
        {
            // Prvo pokupi sve subkategorije koje pripadaju proizvodima tog lokala
            var subcategoryIds = await _context.SubcategoryProducts
                .Where(sp => sp.Product.LocalId == localId)
                .Select(sp => sp.SubcategoryId)
                .Distinct()
                .ToListAsync();

            // Pokupi sve subkategorije koje se koriste
            var subcategories = await _context.Subcategories
                .Where(sc => subcategoryIds.Contains(sc.Id))
                .Select(sc => new
                {
                    sc.Id,
                    sc.Name,
                    sc.Description,
                    sc.CategoryId
                })
                .ToListAsync();

            // Sada sve kategorije koje imaju makar jednu potkategoriju u upotrebi
            var categoryIds = subcategories
                .Select(sc => sc.CategoryId)
                .Distinct()
                .ToList();

            var categories = await _context.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Description
                })
                .ToListAsync();

            // Sastavi rezultat
            var result = categories.Select(cat => new
            {
                id = cat.Id,
                name = cat.Name,
                description = cat.Description,
                subCategories = subcategories
                    .Where(sc => sc.CategoryId == cat.Id)
                    .Select(sub => new
                    {
                        id = sub.Id,
                        name = sub.Name,
                        description = sub.Description
                    })
                    .ToList()
            }).ToList();

            return Ok(result);
        }

        [HttpGet("all/{adminId}")]
        public async Task<IActionResult> GetAllSubcategories(Guid adminId)
        {
            var admin = await _context.Admins
                .Include(a => a.Local)
                .FirstOrDefaultAsync(a => a.Id == adminId);

            if (admin == null)
            {
                return NotFound("Admin not found");
            }

            var subcat = await _context.Subcategories
                .Where(sc => sc.Category.LocalId == admin.LocalId)
                .Select(sc => new SubcategoryDto
                {
                    Id = sc.Id,
                    Name = sc.Name
                }).ToListAsync();

            var subcategories = await _context.Products
                .Where(p => p.LocalId == admin.LocalId)
                .SelectMany(p => p.SubcategoryProducts)
                .Select(sp => new SubcategoryDto
                {
                    Id = sp.Subcategory.Id,
                    Name = sp.Subcategory.Name
                })
                .Distinct()
                .ToListAsync();

            return Ok(subcat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto dto)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LocalId = dto.LocalId
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("by-local/{localId}")]
        public async Task<IActionResult> GetCategoriesByLocal(Guid localId)
        {
            var allSubcategories = await _context.Subcategories
                .Select(sc => new SubCategoryList
                {
                    Id = sc.Id,
                    Name = sc.Name
                }).ToListAsync();

            var categories = await _context.Categories
                .Where(c => c.LocalId == localId)
                .Select(c => new SubCategoriesGetDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    SelectedSubCategoryLists = c.Subcategories
                        .Select(sc => new SubCategoryList
                        {
                            Id = sc.Id,
                            Name = sc.Name
                        }).ToList(),
                    AllSubCategoryLists = allSubcategories
                }).ToListAsync();

            return Ok(categories);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto dto)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = dto.Name;

            category.Subcategories.Clear();

            var selectedSubcategories = await _context.Subcategories
                .Where(sc => dto.SelectedSubCategoryIds.Contains(sc.Id))
                .ToListAsync();

            foreach (var subcategory in selectedSubcategories)
            {
                category.Subcategories.Add(subcategory);
            }


            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("subcategories/{localId}")]
        public async Task<IActionResult> GetSubcategoriesList(Guid localId)
        {
            var local = await _context.Locals
                .FirstOrDefaultAsync(l => l.Id == localId);

            if(local == null)
            {
                return NotFound("Local not found");
            }

            var subcategories = await _context.SubcategoryProducts
                .Where(sc => sc.Product.LocalId == localId)
                .Select(sc => new SubCategoryGetDto
                {
                    Name = sc.Subcategory.Name,
                    Description = sc.Subcategory.Description,
                    Id = sc.Subcategory.Id
                }).ToListAsync();

            return Ok(subcategories);

        }


        #region SubCategories

        [HttpGet("admin/subcategories/{localId}")]
        public async Task<IActionResult> GetSubcategories(Guid localId)
        {

            var local = await _context.Locals
                .FirstOrDefaultAsync(l => l.Id == localId);

            if(local == null)
            {
                return NotFound("Local not found");
            }

            var subcategories = await _context.Subcategories
                .Where(sc => sc.Category.LocalId == localId)
                .Select(sc => new SubCategoryGetDto
                {
                    Name = sc.Name,
                    Description = sc.Description,
                    Id = sc.Id
                }).ToListAsync();

            return Ok(subcategories);

        }

        [HttpPut("admin/subcategories/{id}")]
        public async Task<IActionResult> UpdateSubcategory(Guid id, [FromBody] SubcategoryUpdateDto dto)
        {
            var subcategory = await _context.Subcategories.FindAsync(id);

            if (subcategory == null)
                return NotFound();

            subcategory.Name = dto.Name;
            subcategory.Description = dto.Description;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("admin/subcategories")]
        public async Task<IActionResult> CreateSubcategory([FromBody] SubcategoryCreateDto dto)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == dto.CategoryId);

            if (category == null)
            {
                return NotFound("Category not found");
            }

            var subcategory = new Subcategory
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Category = category
            };

            _context.Subcategories.Add(subcategory);
            await _context.SaveChangesAsync();

            return Ok();
        }


        #endregion

    }
}
