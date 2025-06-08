using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.Domain.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


        public Guid LocalId { get; set; } 
        public Local Local { get; set; } = null!;

        public ICollection<CategorySubcategory> SubcategoryLinks { get; set; } = new List<CategorySubcategory>();
    }

}
