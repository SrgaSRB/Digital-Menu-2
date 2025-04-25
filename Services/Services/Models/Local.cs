using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class Local
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SubscriptionType { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; }
        public ICollection<MenuTheme> MenuThemes { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public Subscription? Subscription { get; set; }

        public Local()
        {
            Products = new List<Product>();
            MenuThemes = new List<MenuTheme>();
            Notifications = new List<Notification>();
        }

    }

}
