namespace Services.DTOs.AdminDTOs
{
    public class SubcategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
    }

}
