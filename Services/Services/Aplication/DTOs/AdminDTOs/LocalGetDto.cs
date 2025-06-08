namespace Services.Aplication.DTOs.AdminDTOs
{
    public class LocalGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime Subscription { get; set; }
    }
}
