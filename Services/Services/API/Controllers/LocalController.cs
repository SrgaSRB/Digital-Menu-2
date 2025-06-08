using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Aplication.DTOs.AdminDTOs;
using Services.Aplication.DTOs.Local;
using Services.Aplication.DTOs.UserMenuDTOs;
using Services.Aplication.Interfaces.Services;
using Services.Infrastructure.Data;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : ControllerBase
    {

        private readonly ILocalService _locSer;

        public LocalController(ILocalService locSer) {
            _locSer = locSer;
        }


        [HttpGet("{localId}/header")]
        public async Task<IActionResult> GetLocalHeader(Guid localId)
            => Ok(await _locSer.GetHeaderAsync(localId));

        [HttpGet("{localId}")]
        public async Task<IActionResult> GetLocalInfo(Guid localId)
            => Ok(await _locSer.GetInfoAsync(localId));   

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocal(Guid id, [FromBody] UpdateLocalDto dto)
        {
            await _locSer.UpdateAsync(dto, id);
            return NoContent();                          
        }

    }
}
