using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ProductNS
{
    public class CategoreyImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string? sourceUrl { get; set; } = string.Empty;
        public string? title { get; set; } = string.Empty;
        public string? altText { get; set; } = string.Empty;

        [ForeignKey(nameof(ProductCategory))]
        public int CategoreyId { get; set; }
        public ProductCategory category { get; set; }
    }
}
