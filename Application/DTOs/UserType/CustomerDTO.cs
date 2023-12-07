using Application.DTOs.UserType;
using Domain.User;

namespace Domain.UserType
{
    public class CustomerDTO : UserTypeDTO
    {
        public Address? Billing { get; set; }
        public Address? Shipping { get; set; }
    }
}
