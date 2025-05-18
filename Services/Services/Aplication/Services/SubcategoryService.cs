using Services.Aplication.DTOs.AdminDTOs;
using Services.Aplication.DTOs.Subcategory;
using Services.Aplication.Interfaces.Repositories;
using Services.Aplication.Interfaces.Services;
using Services.Domain.Models;

namespace Services.Aplication.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _repo;
        private readonly ICategoryRepository _catRepo;
        public SubcategoryService(ISubcategoryRepository repo, ICategoryRepository categoryRepository)
        {
            _repo = repo;
            _catRepo = categoryRepository;
        }

        public async Task<IEnumerable<SubcategoryDto>> GetAllForAdminAsync(Guid adminId)
        {
            var list = await _repo.GetByAdminIdAsync(adminId);
            return list.Select(sc => new SubcategoryDto
            {
                Id = sc.Id,
                Name = sc.Name
            });
        }

        public async Task<IEnumerable<SubcategoryDto>> GetByLocalAsync(Guid localId)
        {
            var list = await _repo.GetByLocalIdAsync(localId);
            return list.Select(sc => new SubcategoryDto
            {
                Id = sc.Id,
                Name = sc.Name,
                Description = sc.Description
            });
        }

        public async Task UpdateAsync(Guid id, SubcategoryUpdateDto dto)
        {
            var subcategory = await _repo.GetAsync(id)
                              ?? throw new KeyNotFoundException("Subcategory not found");

            subcategory.Name = dto.Name;
            subcategory.Description = dto.Description;

            await _repo.SaveAsync();
        }

        public async Task<Guid> CreateAsync(SubcategoryCreateDto dto)
        {
            var category = await _catRepo.GetAsync(dto.CategoryId)
                           ?? throw new KeyNotFoundException("Category not found");

            var subcategory = new Subcategory
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Category = category
            };

            await _repo.AddAsync(subcategory);
            return subcategory.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var sub = await _repo.GetAsync(id)
                      ?? throw new KeyNotFoundException("Subcategory not found");

            await _repo.RemoveAsync(sub);
        }


    }
}
