using Application.DTOs.Product;
using Application.DTOs.ProductDTO;
using Domain.ProductNS;

namespace Application.Repositories
{
    public interface IProductRepository
    {
        public Task<List<SimpleProductDTO>> GetProductsFromCategoryAsync(int CategoeryId);
        public Task<List<SimpleProductDTO>> GetProductsByUserId(int UserId);
        public Task<List<SimpleProductDTO>> GetAllProducts();
        public Task<List<SimpleProductDTO>> GetAllProductsPagination(int pageNumber, int pageSize);

        public Task<SimpleProductDTO> GetProductAsync(int ProductId);
        public Task<SimpleProductDTO> GetProductAsyncBySlugName(string slugName);
        public Task<Product> AddProductAsync(Product product);
        public Task<Product> UpdateProductAsync(Product product);
        public Task<bool> DeleteProductById(int ProductId);
        public Task<Product> AddProductToGategoeryByNameAsync(
            Product product,
            IEnumerable<string> Categories
        );
        public Task<bool> UpdateProductImageAsync(string Mainimage, List<string> Gimages, int id);
    }
}
