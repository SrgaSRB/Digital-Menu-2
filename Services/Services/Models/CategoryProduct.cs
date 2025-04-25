using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class CategoryProduct
    {
        public Guid CategoriesId { get; set; }
        public Guid ProductsId { get; set; }

        public Category Category { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }

}
