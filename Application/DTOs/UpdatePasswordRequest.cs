namespace Application.DTOs
{
    public class UpdatePasswordRequest
    {
        public int userId { get; set; }
        public string oldPassword { get; set; } = string.Empty;
        public string newPassword { get; set; } = string.Empty;
    }

    public class UpdatePasswordRequestDirect
    {
        public string email { get; set; } = string.Empty;
        public string newPassword { get; set; } = string.Empty;
    }
}
