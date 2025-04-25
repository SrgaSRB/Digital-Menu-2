using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Data;

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
            var subQuery =
                from cp in _context.CategoryProducts
                join p in _context.Products on cp.ProductsId equals p.Id
                join c in _context.Categories on cp.CategoriesId equals c.Id
                where p.LocalId == localId
                select new { c.Id, c.Name, c.Description, c.ParentCategoryId };

            var subCategories = await subQuery.Distinct().ToListAsync();

            var parentIds = subCategories
                .Where(sc => sc.ParentCategoryId != null)
                .Select(sc => sc.ParentCategoryId!.Value)
                .Distinct()
                .ToList();

            if (parentIds.Any())
            {
                var parents = await _context.Categories
                    .Where(c => parentIds.Contains(c.Id))
                    .Select(c => new { c.Id, c.Name, c.Description, c.ParentCategoryId })
                    .ToListAsync();

                subCategories.AddRange(parents);  
            }

            var result = subCategories
                .Where(c => c.ParentCategoryId == null)          
                .Select(main => new
                {
                    id = main.Id,
                    name = main.Name,
                    description = main.Description,
                    subCategories = subCategories
                        .Where(sub => sub.ParentCategoryId == main.Id)
                        .Select(sub => new
                        {
                            id = sub.Id,
                            name = sub.Name,
                            description = sub.Description
                        })
                        .ToList()
                })
                .ToList();

            return Ok(result); 
        }



    }
}
