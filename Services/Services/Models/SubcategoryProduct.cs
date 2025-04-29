namespace Services.Models
{
    public class SubcategoryProduct
    {
        public Guid SubcategoryId { get; set; }
        public Guid ProductId { get; set; }

        public Subcategory Subcategory { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }

}
