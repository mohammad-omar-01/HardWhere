using Application.DTOsNS.UserType;
using Domain.UserType;

namespace Application.Repositories
{
    public interface IUserInformationRepository
    {
        Task<CustomerDTO> GetCustomerInfo(string username);
        Task<ViewerDTO> GetViewerInfo(string username);
    }
}
