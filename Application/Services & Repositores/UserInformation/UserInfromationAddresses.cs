using Application.DTOs.Order;
using Application.Repositories;
using Application.Services.UserInformation;
using Domain.UserNS;

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

        public Task<int> GetUserAdressId(int userId)
        {
            return _addressRepository.GetUserAdressId(userId);
        }

        public async Task<AddressReturnResultDTO> UpdateUserAddress(int UserId, AddressDTO address)
        {
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var addressesToReturn = _mapper.Map<Address>(address);
            var addressReturned = await _addressRepository.UpdateUserAddress(
                UserId,
                addressesToReturn
            );
            return _mapper.Map<AddressReturnResultDTO>(addressReturned);
        }
    }
}
