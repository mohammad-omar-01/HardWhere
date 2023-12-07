using System.ComponentModel.DataAnnotations;

namespace Domain.Payment
{
    public class PaymentGateWay
    {
        [Key]
        public string? Id { get; set; }
        public string? title { get; set; }
    }
}
