using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.CartNS;

namespace Domain.Order
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderId { get; set; }

        [Required]
        public int customerId { get; set; }

        [Required]
        public int orderStatus { get; set; }

        public int? adminId { get; set; }

        [Required]
        public DateTime orderDate { get; set; } = DateTime.Now;

        [Required]
        public int total { get; set; }

        [Required]
        public Cart contentes { get; set; }
    }
}
