using Application.DTOs.Product;
using Application.DTOs.ProductDTO;
using Domain.ProductNS;

namespace Application.Services.ProductServiceNS
{
    public interface IProductService
    {
        public Task<List<SimpleProductDTO>> GetsimpleProductsInCategoryAsync(int CategoeryId);
        public Task<List<SimpleProductDTO>> GetAll();
        public Task<List<AdminProductDTO>> GetAllAdmin();
        public Task<bool> ChnageProductStatusByAdmin(int productId, string status);

        public Task<List<AdminProductDTO>> GetsimpleProductspaginatedAdmin(
            int pageNumber,
            int pageSize
        );

        public Task<bool> DeleteProductById(int ProductId);
        public Task<List<SimpleProductDTO>> GetProductsForUserSeraches(int userId);

        public Task<List<SimpleProductDTO>> GetsimpleProductsInByUserId(int UserId);
        public Task<List<SimpleProductDTO>> GetsimpleProductspaginated(
            int pageNumber,
            int pageSize
        );
        public Task<SimpleProductDTO> GetSimpleProductById(int productId);
        public Task<SimpleProductDTO> GetSimpleProductBySlugName(string slugName);
        public Task<Product> AddNewProduct(NewProductRequestDTO product);
        public Task<bool> UpdateProductImages(ProductUpdateImageDTO product);
    }
}
