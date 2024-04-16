using Domain.NotficationNS;

namespace Application.RepositoriesNS
{
    public interface INotficationRepository
    {
        public Task<List<Notfication>> GetNotficationsAsync(int userId);

        public Task<Notfication> CreateNewNotfication(Notfication notfication);
    }
}
