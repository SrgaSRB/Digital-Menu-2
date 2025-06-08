using Services.Aplication.DTOs.Product;
using Services.Aplication.DTOs.UserMenuDTOs;
namespace Services.Aplication.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDto>> GetByLocalIdForUserAsync(Guid localId);
        Task<IEnumerable<GetProductForAdminDto>> GetByLocalIdForAdminSettingsAsync (Guid localId);
        Task UpdateAsync(UpdateProductDto dto, Guid id);
        Task DeleteAsync(Guid id);
        Task CreateAsync(CreateProductDto dto);

    }
}
