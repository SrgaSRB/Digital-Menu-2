using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Services.Domain.Models
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public Guid LocalId { get; set; }
        [ForeignKey("LocalId")]
        public Local Local { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
