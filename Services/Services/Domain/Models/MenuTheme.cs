using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.Domain.Models
{
    public class MenuTheme
    {
        [Key]
        public Guid Id { get; set; }
        public string ThemeName { get; set; } = string.Empty;
        public string CssFileUrl { get; set; } = string.Empty;

        public Guid LocalId { get; set; }
        [ForeignKey("LocalId")]
        public Local Local { get; set; } = new Local();
    }
}
