using Application.DTOs;
using Application.DTOs.User;

namespace Application.Services.Authintication
{
    public interface IUserAuthicticateService
    {
        public bool IsUsernameAvaliable(string username);
        public bool IsEmailAvaliable(string email);
        public string? Login(UserSignInDTO user);
        public Task<bool> UpdatePassword(UpdatePasswordRequest request);
        public Task<bool> UpdatePasswordDirectly(UpdatePasswordRequestDirect request);

        public Task<string> ForgotPassword(string email);
        public bool Logout(LogoutRequestDTO logoutRequest);
        public bool SignUp(UserSignUpDTO user);
    }
}
