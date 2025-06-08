using Services.Domain.Models;

namespace Services.Aplication.Interfaces.Repositories
{
    public interface ISubcaegoryProductRepository
    {
        Task AddAsync(SubcategoryProduct subcategoryProduct);
        Task RemoveAsync(SubcategoryProduct subcategoryProduct);
        Task RemoveByFields(Guid productId, Guid subcategoryId);
        Task SaveAsync();
    }
}
