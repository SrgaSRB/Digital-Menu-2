using Services.Aplication.DTOs.Category;
using Services.Aplication.DTOs.Subcategory;
using Services.Aplication.Exceptions;
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
            var usedSubs = await _subRepo.GetUsedInProductsAsync(localId, ct);

            var dict = new Dictionary<Guid, CategoryWithSubDto>();

            foreach (var sub in usedSubs)
            {
                foreach (var link in sub.CategoryLinks)          
                {
                    var cat = link.Category;

                    if (cat.LocalId != localId) continue;        

                    if (!dict.TryGetValue(cat.Id, out var dto))
                    {
                        dto = new CategoryWithSubDto
                        {
                            Id = cat.Id,
                            Name = cat.Name,
                            Description = cat.Description,
                            Subcategories = new List<SubcategoryDto>()
                        };
                        dict.Add(cat.Id, dto);
                    }

                    dto.Subcategories.Add(new SubcategoryDto
                    {
                        Id = sub.Id,
                        Name = sub.Name,
                        Description = sub.Description
                    });
                }
            }

            return dict.Values;
        }

        public async Task<Guid> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LocalId = dto.LocalId,
                Description = dto.Description == null ? "" : dto.Description
            };

            await _catRepo.AddAsync(category);

            return category.Id;
        }

        public async Task<IEnumerable<SubCategoriesGetDto>> GetCategorySubEditList(Guid localId)
        {
            var categories = await _catRepo.GetByLocalWithSubcategoriesAsync(localId);
            var allSubcategories = await _subRepo.GetBasicByLocalAsync(localId); 

            var allSubList = allSubcategories.Select(sc => new SubCategoryList
            {
                Id = sc.Id,
                Name = sc.Name
            }).ToList();

            return categories.Select(c => new SubCategoriesGetDto
            {
                Id = c.Id,
                Name = c.Name,
                SelectedSubCategoryLists = c.SubcategoryLinks
                    .Select(cs => new SubCategoryList
                    {
                        Id = cs.Subcategory.Id,
                        Name = cs.Subcategory.Name
                    }).ToList(),
                AllSubCategoryLists = allSubList
            });
        }


        public async Task UpdateAsync(Guid id, UpdateCategoryDto dto)
        {
            var category = await _catRepo.GetAsync(id)
                          ?? throw new NotFoundException($"Category {id} not found");

            category.Name = dto.Name;

            category.SubcategoryLinks.Clear();

            category.SubcategoryLinks = dto.SelectedSubCategoryIds
                .Select(subId => new CategorySubcategory
                {
                    CategoryId = category.Id,
                    SubcategoryId = subId
                })
                .ToList();

            await _catRepo.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _catRepo.GetAsync(id)
                          ?? throw new NotFoundException($"Category {id} not found");

            await _catRepo.RemoveAsync(category);
        }

    }
}
