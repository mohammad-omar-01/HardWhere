using Application.DTOs.Order;
using Application.DTOs.UserType;

namespace Application.Services.UserInformation
{
    public interface IUserInformationServiceAddress
    {
        public Task<List<AddressReturnResultDTO>> GetUserAddress(int UserId);
        public Task<AddressReturnResultDTO> UpdateUserAddress(int UserId, AddressDTO address);
    }
}
