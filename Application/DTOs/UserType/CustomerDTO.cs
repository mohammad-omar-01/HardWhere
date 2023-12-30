using Application.DTOsNS.UserType;
using Domain.UserNS;

namespace Domain.UserType
{
    public class CustomerDTO : UserTypeDTO
    {
        public Address? Billing { get; set; }
        public Address? Shipping { get; set; }
    }
}
