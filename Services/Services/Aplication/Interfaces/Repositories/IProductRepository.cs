using Services.Domain.Models;

namespace Services.Aplication.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetAsync(Guid id);
        Task AddAsync(Product product);
        Task RemoveAsync(Guid id);
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task SaveAsync();
        Task<IReadOnlyList<Product>> GetByLocalIdAsync(Guid localId);
        Task<IReadOnlyList<Product>> GetByAdminIdAsync(Guid localId, CancellationToken ct = default);
    }
}
