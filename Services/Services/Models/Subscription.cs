using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class Subscription
    {
        [Key]
        public Guid Id { get; set; }

        public string PlanName { get; set; } = string.Empty; // npr. Starter, Premium
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;

        public Guid LocalId { get; set; }
        [ForeignKey("LocalId")]
        public Local Local { get; set; } = new Local();
    }

}
