using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ProductNS
{
    public class ProductCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public CategoreyImage CategoryImage { get; set; }

        public List<Product>? Products { get; set; }
    }
}
