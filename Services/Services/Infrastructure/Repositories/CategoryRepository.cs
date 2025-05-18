using Microsoft.EntityFrameworkCore;
using Services.Aplication.Interfaces.Repositories;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _ctx;
        public CategoryRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Category category)
        {
            _ctx.Categories.Add(category);
            await SaveAsync();
        }
      
        public Task SaveAsync() => _ctx.SaveChangesAsync();

        public async Task<IReadOnlyList<Category>> GetByLocalWithSubcategoriesAsync(Guid localId)
        {
            return await _ctx.Categories
                .Include(c => c.Subcategories)
                .Where(c => c.LocalId == localId)
                .ToListAsync();
        }

        public async Task<Category?> GetAsync(Guid id)
        {
            return await _ctx.Categories
                .Include(c => c.Subcategories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task RemoveAsync(Category category)
        {
            _ctx.Categories.Remove(category);
            await SaveAsync();
        }

    }
}
