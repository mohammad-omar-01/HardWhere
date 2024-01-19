using Application.RepositoriesNS;
using Domain.NotficationNS;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Repos
{
    public class NotficationRepository : INotficationRepository
    {
        private readonly HardwhereDbContext _dbContext;

        public NotficationRepository(HardwhereDbContext hardwhereDb)
        {
            this._dbContext = hardwhereDb;
        }

        public async Task<Notfication> CreateNewNotfication(Notfication notfication)
        {
            var item = _dbContext.Notfications.Add(notfication).Entity;

            if (item != null)
            {
                return item;
            }
            return null;
        }

        public async Task<List<Notfication>> GetNotficationsAsync(int userId)
        {
            var userToCheck = await _dbContext.Users.FirstOrDefaultAsync(a => a.Id.Equals(userId));
            if (userToCheck != null)
            {
                var notfications = await _dbContext.Notfications
                    .Include(u => u.User)
                    .Where(n => n.userId == userId)
                    .ToListAsync();
                return notfications;
            }
            return null;
        }
    }
}
