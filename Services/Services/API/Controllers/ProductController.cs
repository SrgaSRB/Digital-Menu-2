using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Aplication.DTOs.Product;
using Services.Aplication.Interfaces.Services;
using Services.Domain.Models;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _prodSer;

        public ProductController(IProductService productService) => _prodSer = productService;


        [HttpGet("{localId}")]
        public async Task<IActionResult> GetProducts(Guid localId)
        {
            return Ok(await _prodSer.GetByLocalIdForUserAsync(localId));
        }


        [HttpGet("admin/{localId}")]
        public async Task<IActionResult> GetProductAdmin(Guid localId)
        {
            return Ok(await _prodSer.GetByLocalIdForAdminSettingsAsync(localId));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto dto)
        {
            await _prodSer.UpdateAsync(dto, id);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _prodSer.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            await _prodSer.CreateAsync(dto);
            return Created();
        }


    }
}
