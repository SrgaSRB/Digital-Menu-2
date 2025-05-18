using Microsoft.EntityFrameworkCore;
using Services.Aplication.Interfaces.Repositories;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly AppDbContext _ctx;
        public SubcategoryRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<IReadOnlyList<Subcategory>> GetUsedInProductsAsync(Guid localId, CancellationToken ct = default)
        {
            return await _ctx.SubcategoryProducts
                .Include(sp => sp.Subcategory)
                    .ThenInclude(sc => sc.Category)
                .Where(sp => sp.Product.LocalId == localId)
                .Select(sp => sp.Subcategory)
                .Distinct()
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Subcategory>> GetByAdminIdAsync(Guid adminId, CancellationToken ct = default)
        {
            var admin = await _ctx.Admins
                .Include(a => a.Local)
                .FirstOrDefaultAsync(a => a.Id == adminId, ct);

            if (admin == null || admin.LocalId == Guid.Empty)
                throw new Aplication.Exceptions.UnauthorizedAccessException("Invalid credentials");

            return await _ctx.Subcategories
                .Where(sc => sc.Category.LocalId == admin.LocalId)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Subcategory>> GetAllBasicAsync()
        {
            return await _ctx.Subcategories
                .Select(sc => new Subcategory
                {
                    Id = sc.Id,
                    Name = sc.Name
                }).ToListAsync();
        }

        public async Task<IReadOnlyList<Subcategory>> GetByIdsAsync(List<Guid> ids)
        {
            return await _ctx.Subcategories
                .Where(sc => ids.Contains(sc.Id))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Subcategory>> GetByLocalIdAsync(Guid localId)
        {
            return await _ctx.Subcategories
                .Where(sc => sc.Category.LocalId == localId)
                .ToListAsync();
        }

        public async Task<Subcategory?> GetAsync(Guid id)
        {
            return await _ctx.Subcategories.FindAsync(id);
        }

        public Task SaveAsync()
        {
            return _ctx.SaveChangesAsync();
        }

        public async Task AddAsync(Subcategory subcategory)
        {
            _ctx.Subcategories.Add(subcategory);
            await SaveAsync();
        }

        public async Task RemoveAsync(Subcategory subcategory)
        {
            _ctx.Subcategories.Remove(subcategory);
            await SaveAsync();
        }


    }
}
