using Domain.UserType;

namespace Application.DTOs
{
    public class LoginResult
    {
        public string? Token { get; set; }
        public CustomerDTO? AccountDetails { get; set; }
    }
}
