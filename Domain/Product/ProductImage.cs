using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.ProductNS
{
    public class ProductImage
    {
        public ProductImage() { }

        public ProductImage(int productId, string src, Product product)
        {
            this.ProductId = productId;
            this.SourceUrl = src;
            this.Product = product;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public string SourceUrl { get; set; } = string.Empty;
        public virtual Product Product { get; set; }
    }
}
