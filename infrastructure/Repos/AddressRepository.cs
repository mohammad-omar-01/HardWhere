using Application.Repositories;
using Domain.UserNS;
using Microsoft.EntityFrameworkCore;

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
    }
}
