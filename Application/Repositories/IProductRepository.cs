using Application.DTOs.Product;
using Domain.ProductNS;

namespace Application.Repositories
{
    public interface IProductRepository
    {
        public Task<List<SimpleProductDTO>> GetProductsFromCategoryAsync(int CategoeryId);
        public Task<SimpleProductDTO> GetProductAsync(int ProductId);
        public Task<SimpleProductDTO> GetProductAsyncBySlugName(string slugName);

        public Task<Product> AddProductAsync(Product product);
        public Task<Product> UpdateProductAsync(Product product);
        public Task<Product> AddProductToGategoeryByNameAsync(
            Product product,
            IEnumerable<string> Categories
        );
    }
}
