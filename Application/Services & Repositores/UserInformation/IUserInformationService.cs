using Application.DTOs;
using Application.DTOs.Order;
using Application.DTOs.User;
using Application.DTOs.UserType;

namespace Application.Services.UserInformation
{
    public interface IUserInformationService<T>
    {
        public Task<T> GetUserInformation(UserInfoRequestDTO userInfoRequestDTO);
        public Task<UserInformationDTO> getUserInfo(string username = "", int id = -1);

        public Task<PersonalInformationDTO> UpdateUserInfomation(
            int UserId,
            PersonalInformationDTO pInformation
        );
    }
}
