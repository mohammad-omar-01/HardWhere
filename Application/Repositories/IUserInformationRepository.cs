using Application.DTOs.Order;
using Application.DTOs.UserType;
using Application.DTOsNS.UserType;
using Domain.UserType;

namespace Application.Repositories
{
    public interface IUserInformationRepository
    {
        Task<CustomerDTO> GetCustomerInfo(string username);
        Task<UserInformationDTO> GetUserInformation(string username = "", int id = -1);
        Task<ViewerDTO> GetViewerInfo(string username);
        public PersonalInformationDTO UpdateUserInfomation(
            int UserId,
            PersonalInformationDTO pInformation
        );
    }
}
