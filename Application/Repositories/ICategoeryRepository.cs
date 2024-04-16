using Application.DTOsNS;

namespace Application.Repositories
{
    public interface ICategoeryRepository
    {
        Task<List<CategoeryDTO>> GetCategories();
        Task<CategoeryDTO> GetCategoryBySlugNameAsync(string slugName);
    }
}
