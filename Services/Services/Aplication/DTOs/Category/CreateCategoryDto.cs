using Services.Aplication.DTOs.Subcategory;

namespace Services.Aplication.DTOs.Category
{
    public class CategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid LocalId { get; set; }
        public string? Description { get; set; }
    }
}
