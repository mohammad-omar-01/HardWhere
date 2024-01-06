using Application.DTOs.ProductDTO;
using Domain.ProductNS;

namespace Application.Services.ProductServiceNS
{
    public interface IProductService
    {
        public Task<List<SimpleProductDTO>> GetsimpleProductsInCategoryAsync(int CategoeryId);
        public Task<SimpleProductDTO> GetSimpleProductById(int productId);
        public Task<SimpleProductDTO> GetSimpleProductBySlugName(string slugName);
        public Task<Product> AddNewProduct(NewProductRequestDTO product);
    }
}
