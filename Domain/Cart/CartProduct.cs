using Domain.ProductNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.CartNS
{
    public class CartProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public string CartProductName { get; set; } = string.Empty;
        public int quantity { get; set; }

        public string slug { get; set; } = string.Empty;
        public double price { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public string? ProductImage { get; set; } = string.Empty;
        public virtual Cart cart { get; set; }
    }
}
