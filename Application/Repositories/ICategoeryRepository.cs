using Application.DTOs;
using Domain.Product;

namespace Application.Repositories
{
    public interface ICategoeryRepository
    {
        Task<List<CategoeryDTO>> GetCategories();
    }
}
