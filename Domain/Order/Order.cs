using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.CartNS;
using Domain.UserNS;

namespace Domain.OrderNS
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderId { get; set; }

        [Required]
        public string orderStatus { get; set; } = string.Empty;

        public int customerId { get; set; }

        public int? adminId { get; set; }

        public int? BillingAddressId { get; set; }

        public int? ShippingAdressId { get; set; }

        [Required]
        public DateTime orderDate { get; set; } = DateTime.Now;

        [Required]
        public int total { get; set; }

        public int? shippingTotal { get; set; }

        [Required]
        public virtual User Customer { get; set; }
        public virtual User? admin { get; set; }

        public virtual Address? BillingAdress { get; set; }
        public virtual Address? ShippingAddress { get; set; }
        public virtual List<OrderContents>? contentes { get; set; }
    }
}
