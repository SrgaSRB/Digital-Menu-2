namespace Services.DTOs.AdminDTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AdditionalDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public Guid AdminId { get; set; }
        public List<Guid> SelectedSubCategoryIds { get; set; } = new List<Guid>();
    }

}
