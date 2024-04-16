using Application.DTOs;
using Domain.UserNs;

namespace Application.Services___Repositores
{
    public interface ISearchService
    {
        public Task<UserSearch> AddSearchRecord(SerachQuery query);
    }
}
