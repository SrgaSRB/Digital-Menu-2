using Microsoft.EntityFrameworkCore;
using Services.Aplication.Interfaces.Repositories;
using Services.Domain.Models;
using Services.Infrastructure.Data;

namespace Services.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {

        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetAdminByUsernameAsync(string username, CancellationToken ct)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == username)
                ?? throw new Aplication.Exceptions.NotFoundException($"Admin with username: {username} not found");

            return admin;
        }
    }
}
