namespace Services.DTOs.AdminDTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public Guid Id { get; set; }
        public bool IsSuperAdmin { get; set; }
        public Guid LocalId { get; set; }
    }
}
