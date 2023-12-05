using Domain.User;
using System.Net;

namespace Domain.Authintication
{
    public class Customer
    {
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public int? DatabaseId { get; set; }
        public string SessionToken { get; set; }
        public Address? Billing { get; set; }
        public Address? Shipping { get; set; }
    }
}
