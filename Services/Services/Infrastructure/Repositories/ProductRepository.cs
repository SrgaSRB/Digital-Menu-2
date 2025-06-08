using Microsoft.EntityFrameworkCore;
using Services.Aplication.Interfaces.Repositories;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.SubcategoryProducts)
                    .ThenInclude(sp => sp.Subcategory)
                .ToListAsync();
        }

        public async Task<Product?> GetAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.SubcategoryProducts)
                    .ThenInclude(sp => sp.Subcategory)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Product>> GetByLocalIdAsync(Guid localId)
        {
             return await _context.Products
                .Include(p => p.SubcategoryProducts)
                    .ThenInclude(sp => sp.Subcategory)
                .Where(p => p.LocalId == localId)
                .ToListAsync();
        }

        public Task<IReadOnlyList<Product>> GetByAdminIdAsync(Guid localId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
