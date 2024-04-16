using Application.DTOs.Notfication;
using Application.RepositoriesNS;
using Domain.CartNS;
using Domain.NotficationNS;

namespace Application.Services___Repositores.NotficationNS
{
    public class NotficationService : INotficationService
    {
        private readonly INotficationRepository _notficationRepository;

        public NotficationService(INotficationRepository notficationRepository)
        {
            this._notficationRepository = notficationRepository;
        }

        public async Task<Notfication> CreateNotfication(NotficationDTO NotficationToCreate)
        {
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var notficationToAdd = _mapper.Map<Notfication>(NotficationToCreate);
            notficationToAdd.slug = "";
            var addedNotfication = await _notficationRepository.CreateNewNotfication(
                notficationToAdd
            );
            return addedNotfication;
        }

        public Task<Notfication> DeleteNotfication()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Notfication>> GetAllNotfications(int userId)
        {
            var addedNotfication = await _notficationRepository.GetNotficationsAsync(userId);
            return addedNotfication;
        }
    }
}
