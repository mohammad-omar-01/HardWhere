using Application.DTOs.Order;

namespace Application.Services.UserInformation
{
    public interface IUserInformationServiceAddress
    {
        public Task<List<AddressReturnResultDTO>> GetUserAddress(int UserId);
    }
}
