namespace Services.DTOs.AdminDTOs
{
    public class LocalGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Subscription { get; set; }
    }
}
