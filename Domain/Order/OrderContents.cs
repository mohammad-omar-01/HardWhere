using Domain.CartNS;
using Domain.ProductNS;

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.OrderNS
{
    public class OrderContents
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public string OrderProductName { get; set; } = string.Empty;
        public int quantity { get; set; }

        public string slug { get; set; } = string.Empty;
        public int price { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public string? ProductImage { get; set; } = string.Empty;
        public virtual Order order { get; set; }
    }
}
