using Application.DTOs;
using Application.RepositoriesNS;
using Domain.UserNs;

namespace infrastructure.Repos
{
    public class SearchRepository : ISearchRepository
    {
        private readonly HardwhereDbContext _dbContext;

        public SearchRepository(HardwhereDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserSearch> AddNewSearchRecord(SerachQuery que)
        {
            string query = que.Query;
            int userId = que.UserId;
            var result = _dbContext.UserSearch.FirstOrDefault(record => record.userId == userId);
            if (result == null)
            {
                UserSearch userSearch = new UserSearch();
                userSearch.userId = userId;
                userSearch.serachKeywords.Add(query);
                var resultUser = _dbContext.UserSearch.Add(userSearch);
                _dbContext.SaveChanges();

                return Task.FromResult(resultUser.Entity);
            }
            var isExist = result.serachKeywords.FirstOrDefault(a => a.Contains(query));
            if (isExist == null)
            {
                result.serachKeywords.Add(query);
                _dbContext.SaveChanges();
                return Task.FromResult(result);
            }

            return (null);
        }
    }
}
