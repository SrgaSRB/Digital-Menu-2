namespace Services.DTOs.AdminDTOs
{
    public class CategoryCreateDto
    {
        public Guid LocalId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
