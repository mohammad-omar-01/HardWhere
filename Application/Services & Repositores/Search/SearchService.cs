using Application.DTOs;
using Application.Repositories;
using Application.RepositoriesNS;
using Domain.UserNs;

namespace Application.Services___Repositores.Search
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepo;

        public SearchService(ISearchRepository searchRepo)
        {
            this._searchRepo = searchRepo;
        }

        public Task<UserSearch> AddSearchRecord(SerachQuery query)
        {
            return _searchRepo.AddNewSearchRecord(query);
        }
    }
}
