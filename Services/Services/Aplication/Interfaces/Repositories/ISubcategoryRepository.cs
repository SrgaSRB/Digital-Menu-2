using Services.Domain.Models;

namespace Services.Aplication.Interfaces.Repositories
{
    public interface ISubcategoryRepository
    {
        Task<IReadOnlyList<Subcategory>> GetUsedInProductsAsync(Guid localId, CancellationToken ct = default);
        Task<IReadOnlyList<Subcategory>> GetByAdminIdAsync(Guid adminId, CancellationToken ct = default);
        Task<IReadOnlyList<Subcategory>> GetAllBasicAsync();
        Task<IReadOnlyList<Subcategory>> GetByIdsAsync(List<Guid> ids);
        Task<IReadOnlyList<Subcategory>> GetByLocalIdAsync(Guid localId);
        Task<Subcategory?> GetAsync(Guid id);
        Task SaveAsync();
        Task AddAsync(Subcategory subcategory);
        Task RemoveAsync(Subcategory subcategory);
    }
}
