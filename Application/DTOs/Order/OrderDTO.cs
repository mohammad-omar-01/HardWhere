using Application.DTOs.Cart;
using Domain.OrderNS;
using Domain.UserNS;
using static Domain.Enums.CounteryEnum;

namespace Application.DTOs.Order
{
    public class OrderDTO
    {
        public string total { get; set; } = string.Empty;
        public int userId { get; set; }
        public double shipptingTotal { get; set; }
        public double? totalInt { get; set; }
        public int? shippingAddressId { get; set; }
        public int? billingAddressId { get; set; }
        public List<OrderItem> contents { get; set; }
    }

    public class OrderItem
    {
        public int? quantity { get; set; }
        public string priceRegular { get; set; } = string.Empty;
        public ProductRequestDTO product { get; set; }
        public double? totalPriceForTheseProducts { get; set; }
    }

    public class UserDTOForAdress
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class AddressReturnResultDTO
    {
        public UserDTOForAdress? user { get; set; }
        public int AddressID { get; set; }
        public string AddressName { get; set; } = "Other";
        public int userId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public String? Country { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
    }

    public class AddressDTO
    {
        public string AddressName { get; set; } = "Other";
        public UserDTOForAdress? user { get; set; }
        public int userId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public String? Country { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
    }
}
