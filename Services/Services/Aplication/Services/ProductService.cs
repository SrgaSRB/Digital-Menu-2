using Services.Aplication.DTOs.Product;
using Services.Aplication.DTOs.UserMenuDTOs;
using Services.Aplication.Interfaces.Repositories;
using Services.Aplication.Interfaces.Services;
using Services.Domain.Models;

namespace Services.Aplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _prodRepo;
        private readonly ISubcaegoryProductRepository _subCatProdRepo;
        private readonly ISubcategoryRepository _subCategoryRepo;

        public ProductService(IProductRepository productRepository, ISubcaegoryProductRepository subcategoryProductRepository, ISubcategoryRepository subcategoryRepository)
        {
            _prodRepo = productRepository;
            _subCatProdRepo = subcategoryProductRepository;
            _subCategoryRepo = subcategoryRepository;
        }

        public async Task CreateAsync(CreateProductDto dto)
        {
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                AdditionalDescription = dto.AdditionalDescription,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                HaveImage = !string.IsNullOrEmpty(dto.ImageUrl),
                IsDeleted = dto.IsDeleted,
                LocalId = dto.LocalId,
            };

            foreach (var subcatId in dto.SelectedSubCategoryIds)
            {
                await _subCatProdRepo.AddAsync(new SubcategoryProduct
                {
                    ProductId = newProduct.Id,
                    SubcategoryId = subcatId
                });
            }

            await _prodRepo.AddAsync(newProduct);

            await _prodRepo.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _prodRepo.RemoveAsync(id);
            await _prodRepo.SaveAsync();
        }

        public async Task<IEnumerable<GetProductForAdminDto>> GetByLocalIdForAdminSettingsAsync(Guid localId)
        {
            var products = await _prodRepo.GetByLocalIdAsync(localId);

            var allSubcategories = await _subCategoryRepo.GetAllBasicAsync();

            var retProducts = products
                .Select(p => new GetProductForAdminDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    AdditionalDescription = p.AdditionalDescription,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    IsDeleted = p.IsDeleted,
                    SelectedSubCategories = p.SubcategoryProducts
                        .Select(sp => new SubcategoryDto
                        {
                            Id = sp.SubcategoryId,
                            Name = sp.Subcategory.Name,
                        })
                        .ToList(),
                    HaveImage = p.HaveImage,
                    AllSubCategories = allSubcategories
                        .Select(sc => new SubcategoryDto
                        {
                            Id = sc.Id,
                            Name = sc.Name
                        })
                        .ToList()
                });

            return retProducts;
        }

        public async Task<IEnumerable<GetProductDto>> GetByLocalIdForUserAsync(Guid localId)
        {
            var products = await _prodRepo.GetByLocalIdAsync(localId);

            return products.Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                AdditionalDescription = p.AdditionalDescription,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                HaveImage = p.HaveImage,
                Categories = p.SubcategoryProducts
                    .Select(sp => sp.SubcategoryId)
                    .ToList()
            });
        }

        public async Task UpdateAsync(UpdateProductDto dto, Guid id)
        {
            var product = await _prodRepo.GetAsync(id)
                ?? throw new Exceptions.NotFoundException($"Product {id} not found");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.AdditionalDescription = dto.AdditionalDescription;
            product.Price = dto.Price;
            product.IsDeleted = dto.IsDeleted;

            var existingLinks = product.SubcategoryProducts.ToList();
            foreach (var link in existingLinks)
                await _subCatProdRepo.RemoveByFields(product.Id, link.SubcategoryId);

            foreach (var subcatId in dto.SelectedSubCategoryIds)
            {
                await _subCatProdRepo.AddAsync(new SubcategoryProduct
                {
                    ProductId = product.Id,
                    SubcategoryId = subcatId
                });
            }

            if (string.IsNullOrWhiteSpace(dto.ImageUrl))
            {
                product.ImageUrl = null!;
                product.HaveImage = false;
            }
            else
            {
                product.ImageUrl = dto.ImageUrl;
                product.HaveImage = true;
            }

            await _prodRepo.SaveAsync();
        }
    }
}
