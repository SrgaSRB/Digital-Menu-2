using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.Domain.Models
{
    public class Local
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SubscriptionType { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public bool HaveLogo { get; set; } = false;

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<MenuTheme> MenuThemes { get; set; } = new List<MenuTheme>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public Subscription? Subscription { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
    }

}
