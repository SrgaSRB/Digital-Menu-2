using Services.Aplication.DTOs.Category;
using Services.Aplication.DTOs.Subcategory;

namespace Services.Aplication.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryWithSubDto>> GetActiveCategoriesWithSubAsync(Guid localId, CancellationToken ct = default);
        Task<Guid> CreateAsync(CategoryCreateDto dto);
        Task<IEnumerable<SubCategoriesGetDto>> GetCategorySubEditList(Guid localId);
        Task UpdateAsync(Guid id, UpdateCategoryDto dto);
        Task DeleteAsync(Guid id);

    }
}
