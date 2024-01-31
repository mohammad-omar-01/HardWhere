using Application.DTOsNS;
using Application.Repositories;
using Domain.ProductNS;

namespace Application.Services.Categoery
{
    public class CategoeryService : ICategoeryService
    {
        private readonly ICategoeryRepository _categoeryRepository;

        public CategoeryService(ICategoeryRepository categoeryRepository)
        {
            this._categoeryRepository = categoeryRepository;
        }

        public async Task<List<CategoeryDTO>> GetCategories()
        {
            return await _categoeryRepository.GetCategories();
        }

        public async Task<CategoeryDTO> GetCategoryBySlugNameAsync(string slugName)
        {
            var response = await _categoeryRepository.GetCategoryBySlugNameAsync(slugName);
            return response;
        }
    }
}
