using Application.DTOs;
using Domain.UserNs;

namespace Application.RepositoriesNS
{
    public interface ISearchRepository
    {
        public Task<UserSearch> AddNewSearchRecord(SerachQuery query);
    }
}
