using Application.DTOsNS;
using Domain.ProductNS;

namespace Application.Services.Categoery
{
    public interface ICategoeryService
    {
        Task<List<CategoeryDTO>> GetCategories();
        Task <CategoeryDTO> GetCategoryBySlugNameAsync(string slugName);
    }
}
