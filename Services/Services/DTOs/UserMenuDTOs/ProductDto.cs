namespace Services.DTOs.UserMenuDTOs
{
    public class ProductDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AdditionalDescription { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public string ImageUrl { get; set; } = string.Empty;
        public List<Guid> Categories { get; set; } = new List<Guid>();
    }
}
