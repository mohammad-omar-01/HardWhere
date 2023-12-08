using Application.DTOs;
using Domain.Product;

namespace Application.Services.Categoery
{
    public interface ICategoeryService
    {
        Task<List<CategoeryDTO>> GetCategories();
    }
}
