using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Aplication.DTOs.Category;
using Services.Aplication.DTOs.Subcategory;
using Services.Aplication.Interfaces.Services;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service) => _service = service;


        [HttpGet("active-with-subcategories/{localId:guid}")]
        public async Task<IActionResult> GetActiveCategories(Guid localId, CancellationToken ct = default)
        {
            var result = await _service.GetActiveCategoriesWithSubAsync(localId, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(id);
        }

        [HttpGet("by-local/{localId:guid}")]
        public async Task<IActionResult> GetCategoriesByLocal(Guid localId)
        {
            var result = await _service.GetCategorySubEditList(localId);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Category not found");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Category not found");
            }
        }

    }
}
