using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Services.Models
{
    public class Admin
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "admin"; // or "superadmin"


        public Guid LocalId { get; set; }
        [ForeignKey("LocalId")]
        public Local Local { get; set; } = new Local();
    }

}
