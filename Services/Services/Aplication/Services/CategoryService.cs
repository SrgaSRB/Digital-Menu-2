using Services.Aplication.DTOs.Category;
using Services.Aplication.DTOs.Subcategory;
using Services.Aplication.Interfaces.Repositories;
using Services.Aplication.Interfaces.Services;
using Services.Domain.Models;
using CategoryCreateDto = Services.Aplication.DTOs.Category.CategoryCreateDto;

namespace Services.Aplication.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _catRepo;
        private readonly ISubcategoryRepository _subRepo;

        public CategoryService(
            ICategoryRepository catRepo,
            ISubcategoryRepository subRepo)
        {
            _catRepo = catRepo;
            _subRepo = subRepo;
        }

        public async Task<IEnumerable<CategoryWithSubDto>> GetActiveCategoriesWithSubAsync(Guid localId, CancellationToken ct = default)
        {
            var usedSubcategories = await _subRepo.GetUsedInProductsAsync(localId);

            var grouped = usedSubcategories
                .GroupBy(sc => sc.Category)
                .Select(group => new CategoryWithSubDto
                {
                    Id = group.Key.Id,
                    Name = group.Key.Name,
                    Description = group.Key.Description,
                    Subcategories = group.Select(sc => new SubcategoryDto
                    {
                        Id = sc.Id,
                        Name = sc.Name,
                        Description = sc.Description
                    }).ToList()
                });

            return grouped;
        }

        public async Task<Guid> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LocalId = dto.LocalId,
                Description = dto.Description
            };

            await _catRepo.AddAsync(category);

            return category.Id;
        }

        public async Task<IEnumerable<SubCategoriesGetDto>> GetCategorySubEditList(Guid localId)
        {
            var categories = await _catRepo.GetByLocalWithSubcategoriesAsync(localId);
            var allSubcategories = await _subRepo.GetAllBasicAsync();

            var allSubList = allSubcategories.Select(sc => new SubCategoryList
            {
                Id = sc.Id,
                Name = sc.Name
            }).ToList();

            return categories.Select(c => new SubCategoriesGetDto
            {
                Id = c.Id,
                Name = c.Name,
                SelectedSubCategoryLists = c.Subcategories.Select(sc => new SubCategoryList
                {
                    Id = sc.Id,
                    Name = sc.Name
                }).ToList(),
                AllSubCategoryLists = allSubList
            });
        }

        public async Task UpdateAsync(Guid id, UpdateCategoryDto dto)
        {
            var category = await _catRepo.GetAsync(id)
                          ?? throw new KeyNotFoundException("Category not found");

            category.Name = dto.Name;

            category.Subcategories.Clear();

            var selectedSubcategories = await _subRepo.GetByIdsAsync(dto.SelectedSubCategoryIds);

            foreach (var sub in selectedSubcategories)
            {
                category.Subcategories.Add(sub);
            }

            await _catRepo.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _catRepo.GetAsync(id)
                          ?? throw new KeyNotFoundException("Category not found");

            await _catRepo.RemoveAsync(category);
        }

    }
}
