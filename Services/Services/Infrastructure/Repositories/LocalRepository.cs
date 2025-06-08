using Microsoft.EntityFrameworkCore;
using Services.Aplication.DTOs.Local;
using Services.Aplication.DTOs.UserMenuDTOs;
using Services.Aplication.Interfaces.Repositories;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories
{
    public class LocalRepository : ILocalRepository
    {
        private readonly AppDbContext _context;

        public LocalRepository(AppDbContext appDbContext) => _context = appDbContext;

        public Task<Local?> GetAsync(Guid id)
            => _context.Locals
                   .Include(l => l.Subscription)  
                   .FirstOrDefaultAsync(l => l.Id == id);

        public async Task<GetLocalHeaderDto?> GetHeaderAsync(Guid id)
        {
            var header = await _context.Locals
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (header == null)
            {
                throw new Aplication.Exceptions.NotFoundException($"Local with ID {id} not found.");
            }

            return new GetLocalHeaderDto
            {
                ImageUrl = header.LogoUrl,
                Name = header.Name
            };
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}
