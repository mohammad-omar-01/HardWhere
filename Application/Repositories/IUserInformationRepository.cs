using Application.DTOs.UserType;
using Domain.UserType;

namespace Application.Repositories
{
    public interface IUserInformationRepository
    {
        Task<CustomerDTO> GetCustomerInfo(string username);
        Task<ViewerDTO> GetViewerInfo(string username);
    }
}
