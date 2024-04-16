using Application.DTOs.Cart;
using Domain.UserNS;

namespace Application.DTOs.Order
{
    public class OrderDtoReturnResult
    {
        public OrderDTO result { get; set; }
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public string OrderDate { get; set; }
        public AddressReturnResultDTO? billing { get; set; }
        public AddressReturnResultDTO? shipping { get; set; }
    }

    public class OrderDtoReturnResultList
    {
        public List<OrderDtoReturnResult> result { get; set; }
    }
}
