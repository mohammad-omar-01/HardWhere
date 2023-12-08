using Application.DTOs;
using Application.Repositories;
using Domain.Product;

namespace Application.Services.Categoery
{
    public class CategoeryService : ICategoeryService
    {
        private readonly ICategoeryRepository _categoeryRepository;

        public CategoeryService(ICategoeryRepository categoeryRepository) {
        
        this._categoeryRepository=categoeryRepository;
        }
        public async Task<List<CategoeryDTO>> GetCategories()
        {
            return await _categoeryRepository.GetCategories();
        }
    }
}
