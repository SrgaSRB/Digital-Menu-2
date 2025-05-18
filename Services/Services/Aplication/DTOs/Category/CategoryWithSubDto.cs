using Services.Aplication.DTOs.Subcategory;

namespace Services.Aplication.DTOs.Category
{
    public class CategoryWithSubDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<SubcategoryDto> Subcategories { get; set; } = new();
    }
}
