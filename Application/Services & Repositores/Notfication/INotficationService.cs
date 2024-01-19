using Application.DTOs.Notfication;
using Domain.NotficationNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services___Repositores.NotficationNS
{
    public interface INotficationService
    {
        public Task<Notfication> CreateNotfication(NotficationDTO NotficationToCreate);
        public Task<Notfication> DeleteNotfication();
        public Task<List<Notfication>> GetAllNotfications(int userId);
    }
}
