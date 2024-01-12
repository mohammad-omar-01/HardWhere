using Application.DTOs.User;

namespace Application.Services.UserInformation
{
    public interface IUserInformationService<T>
    {
        public Task<T> GetUserInformation(UserInfoRequestDTO userInfoRequestDTO);
    }
}
