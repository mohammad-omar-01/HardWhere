namespace Application.DTOs.UserType
{
    public class UserInformationDTO
    {
        public PersonalInformationDTO? PersonalInformation { get; set; }

        public BillingAdressDTO? billing { get; set; }

        public ShippingAdressDTO? shipping { get; set; }
    }

    public class PersonalInformationDTO
    {
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? firstName { get; set; }
    }

    public class BillingAdressDTO
    {
        public int addressId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? address1 { get; set; }
        public string? address2 { get; set; }
        public string? city { get; set; }
        public String? country { get; set; }
        public string? phone { get; set; }
        public string? state { get; set; }
    }

    public class ShippingAdressDTO
    {
        public int addressId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? address1 { get; set; }
        public string? address2 { get; set; }
        public string? city { get; set; }
        public String? country { get; set; }
        public string? phone { get; set; }
        public string? state { get; set; }
    }
}
