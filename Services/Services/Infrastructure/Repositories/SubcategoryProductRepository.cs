using Services.Aplication.Interfaces.Repositories;
using Services.Domain.Models;
using Services.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.Infrastructure.Repositories
{
    public class SubcategoryProductRepository : ISubcaegoryProductRepository
    {
        private readonly AppDbContext _ctx;
        public SubcategoryProductRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(SubcategoryProduct subcategoryProduct)
        {
            await _ctx.SubcategoryProducts.AddAsync(subcategoryProduct);
        }

        public async Task RemoveAsync(SubcategoryProduct subcategoryProduct)
        {
            await _ctx.SubcategoryProducts
                .Where(sp => sp.SubcategoryId == subcategoryProduct.SubcategoryId && sp.ProductId == subcategoryProduct.ProductId)
                .ExecuteDeleteAsync();
        }

        public async Task RemoveByFields(Guid productId, Guid subcategoryId)
        {
            await _ctx.SubcategoryProducts
                .Where(sp => sp.SubcategoryId == subcategoryId && sp.ProductId == productId)
                .ExecuteDeleteAsync();
        }

        public async Task SaveAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
