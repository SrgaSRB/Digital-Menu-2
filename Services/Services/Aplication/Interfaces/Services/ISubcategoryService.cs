using Services.Aplication.DTOs.AdminDTOs;
using Services.Aplication.DTOs.Category;
using Services.Aplication.DTOs.Subcategory;

namespace Services.Aplication.Interfaces.Services
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<SubcategoryDto>> GetAllForAdminAsync(Guid adminId);
        Task<IEnumerable<SubcategoryDto>> GetByLocalAsync(Guid localId);
        Task UpdateAsync(Guid id, SubcategoryUpdateDto dto);
        Task<Guid> CreateAsync(SubcategoryCreateDto dto);
        Task DeleteAsync(Guid id);

    }
}
