using Services.Aplication.DTOs.AdminDTOs;
using Services.Aplication.DTOs.Subcategory;
using Services.Aplication.Exceptions;
using Services.Aplication.Interfaces.Repositories;
using Services.Aplication.Interfaces.Services;
using Services.Domain.Models;

namespace Services.Aplication.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _subRepo;
        private readonly ICategoryRepository _catRepo;
        public SubcategoryService(ISubcategoryRepository repo, ICategoryRepository categoryRepository)
        {
            _subRepo = repo;
            _catRepo = categoryRepository;
        }

        public async Task<IEnumerable<SubcategoryDto>> GetAllForAdminAsync(Guid adminId)
        {
            var list = await _subRepo.GetByAdminIdAsync(adminId);
            return list.Select(sc => new SubcategoryDto
            {
                Id = sc.Id,
                Name = sc.Name
            });
        }

        public async Task<IEnumerable<SubcategoryDto>> GetByLocalAsync(Guid localId)
        {
            var list = await _subRepo.GetByLocalIdAsync(localId);
            return list.Select(sc => new SubcategoryDto
            {
                Id = sc.Id,
                Name = sc.Name,
                Description = sc.Description
            });
        }

        public async Task UpdateAsync(Guid id, SubcategoryUpdateDto dto)
        {
            var subcategory = await _subRepo.GetAsync(id)
                              ?? throw new NotFoundException("Subcategory not found");

            subcategory.Name = dto.Name;
            subcategory.Description = dto.Description ?? string.Empty;

            await _subRepo.SaveAsync();
        }

        public async Task<Guid> CreateAsync(SubcategoryCreateDto dto)
        {
            var category = await _catRepo.GetAsync(dto.CategoryId)
                          ?? throw new NotFoundException($"Category {dto.CategoryId} not found");

            var subcategoryId = Guid.NewGuid();

            var subcategory = new Subcategory
            {
                Id = subcategoryId,
                LocalId = category.LocalId,       
                Name = dto.Name,
                Description = dto.Description,
                CategoryLinks = new List<CategorySubcategory>
                {
                    new CategorySubcategory
                    {
                        CategoryId    = category.Id,
                        SubcategoryId = subcategoryId   
                    }
                }
            };

            await _subRepo.AddAsync(subcategory);
            return subcategory.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var sub = await _subRepo.GetAsync(id)
                      ?? throw new NotFoundException("Subcategory not found");

            await _subRepo.RemoveAsync(sub);
        }


    }
}
