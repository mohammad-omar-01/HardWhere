using Application.DTOs.Order;
using Application.Repositories;
using Application.Services.UserInformation;
using Domain.Enums;
using Domain.OrderNS;

namespace Application.Services___Repositores.UserInformation
{
    public class UserInfromationAddresses : IUserInformationServiceAddress
    {
        private readonly IAddress _addressRepository;

        public UserInfromationAddresses(IAddress addressRepository)
        {
            this._addressRepository = addressRepository;
        }

        public async Task<List<AddressReturnResultDTO>> GetUserAddress(int UserId)
        {
            var addresses = await _addressRepository.GetUserAdresses(UserId);
            if (addresses == null)
            {
                return null;
            }
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var addressesToReturn = _mapper.Map<List<AddressReturnResultDTO>>(addresses);

            return addressesToReturn;
        }
    }
}
