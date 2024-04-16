using Application.DTOs.Order;
using Domain.UserNS;

namespace Application.Repositories
{
    public interface IAddress
    {
        public Task<List<Address>> GetUserAdresses(int userId);
        public Task<int> GetUserAdressId(int userId);
        public Task<Address> UpdateUserAddress(int userId, Address address);
    }
}
