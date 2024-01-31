using Domain.ProductNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ProductCartNs
{
    public class CartProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        [ForeignKey(nameof(Product))]
        public int productId { get; set; }
        public virtual ProductImage? CartImage { get; set; }
        public int Price { get; set; }
    }
}
