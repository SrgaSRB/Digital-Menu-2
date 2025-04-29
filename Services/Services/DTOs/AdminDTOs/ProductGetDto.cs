using System.Globalization;

namespace Services.DTOs.AdminDTOs
{
    public class ProductGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AdditionalDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public bool HaveImage { get; set; }

        public List<SubcategoryDto> SelectedSubCategories { get; set; } = new List<SubcategoryDto>();
        public List<SubcategoryDto> AllSubCategories { get; set; } = new List<SubcategoryDto>();
    }

}
