using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Guid? ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category? ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set; } = new List<Category>();
        public ICollection<CategoryProduct> CategoryProducts { get; set; } = new List<CategoryProduct>();


    }
}
