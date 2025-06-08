namespace Services.Domain.Models
{
    public class CategorySubcategory
    {
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }

        public Category Category { get; set; } = null!;
        public Subcategory Subcategory { get; set; } = null!;
    }
}
