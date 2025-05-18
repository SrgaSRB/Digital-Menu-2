using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Aplication.DTOs.AdminDTOs;
using Services.Aplication.DTOs.Subcategory;
using Services.Aplication.Interfaces.Services;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly ISubcategoryService _service;
        public SubcategoriesController(ISubcategoryService service) => _service = service;

        [HttpGet("all-by-admin/{adminId:guid}")]
        public async Task<IActionResult> GetAllByAdmin(Guid adminId)
        {
            var subcategories = await _service.GetAllForAdminAsync(adminId);
            return Ok(subcategories);
        }

        [HttpGet("by-local/{localId:guid}")]
        public async Task<IActionResult> GetByLocal(Guid localId)
        {
            var result = await _service.GetByLocalAsync(localId);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SubcategoryUpdateDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Subcategory not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubcategoryCreateDto dto)
        {
            try
            {
                var id = await _service.CreateAsync(dto);
                return Ok(id);
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
                return Ok("Subcategory successfully deleted");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Subcategory not found");
            }
        }


    }
}
