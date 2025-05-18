using Services.Domain.Models;

namespace Services.Aplication.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task SaveAsync();
        Task<IReadOnlyList<Category>> GetByLocalWithSubcategoriesAsync(Guid localId);
        Task<Category?> GetAsync(Guid id);
        Task RemoveAsync(Category category);

    }
}
