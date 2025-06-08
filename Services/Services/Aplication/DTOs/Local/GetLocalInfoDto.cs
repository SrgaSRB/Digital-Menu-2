namespace Services.Aplication.DTOs.Local
{
    public class GetLocalInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime? Subscription { get; set; }
    }
}
