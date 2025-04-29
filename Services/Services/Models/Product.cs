using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AdditionalDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool HaveImage { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public Guid LocalId { get; set; }
        [ForeignKey("LocalId")]
        public Local Local { get; set; } = null!;

        public ICollection<SubcategoryProduct> SubcategoryProducts { get; set; } = new List<SubcategoryProduct>();
    }

}
