using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Product
{
    public class CategoreyImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string? sourceUrl { get; set; }
        public string? title { get; set; }
        public string? altText { get; set; }

        [ForeignKey(nameof(ProductCategory))]
        public int CategoreyId { get; set; }
        public ProductCategory category { get; set; }
    }
}
