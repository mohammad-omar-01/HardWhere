using Application.Repositories;
using Domain.UserNS;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace infrastructure.Repos
{
    public class AddressRepository : IAddress
    {
        private readonly HardwhereDbContext _dbContext;

        public AddressRepository(HardwhereDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Address>> GetUserAdresses(int userId)
        {
            var result = await _dbContext.Addresses
                .Include(a => a.user)
                .Where(a => a.userId == userId)
                .ToListAsync();

            return result;
        }

        public Task<int> GetUserAdressId(int userId)
        {
            var result = _dbContext.Addresses.FirstOrDefault(a => a.userId == userId).AddressID;

            return Task.FromResult(result);
        }

        public async Task<Address> UpdateUserAddress(int userId, Address address)
        {
            var adressReturnd = await _dbContext.Addresses.FirstOrDefaultAsync(
                a => a.userId == userId
            );
            if (address != null)
            {
                foreach (
                    PropertyInfo property in typeof(Address).GetProperties().Where(p => p.CanWrite)
                )
                {
                    if (property.Name.Equals("AddressID"))
                        continue;
                    if (property.Name.Equals("userId"))
                        continue;
                    if (property.Name.Equals("user"))
                        continue;
                    property.SetValue(adressReturnd, property.GetValue(address, null), null);
                }
                await _dbContext.SaveChangesAsync();
                return adressReturnd;
            }
            return address;
        }
    }
}
