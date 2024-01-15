using Application.DTOs;
using Application.DTOs.ProductDTO;
using Application.Repositories;
using Application.Utilities;
using Domain.Enums;
using Domain.ProductNS;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Application.Services.ProductServiceNS
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        private readonly IStringRandomGenarotor<SlugGenerator> _slugGenerator;
        private readonly IStringRandomGenarotor<SKUGenerator> _skuGenerator;

        public ProductService(
            IProductRepository productRepository,
            IFileService fileService,
            IStringRandomGenarotor<SlugGenerator> slug,
            IStringRandomGenarotor<SKUGenerator> sku
        )
        {
            _productRepository = productRepository;
            _fileService = fileService;
            _slugGenerator = slug;
            _skuGenerator = sku;
        }

        public async Task<Product> AddNewProduct(NewProductRequestDTO product)
        {
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var productToAdd = _mapper.Map<Product>(product);
            productToAdd.DateAdded = DateTime.Now;
            productToAdd.StockStatus = StockStatusEnum.IN_STOCK.ToString();
            productToAdd.Sku = _skuGenerator.GenrateRandomString(productToAdd);
            productToAdd.Slug = _slugGenerator.GenrateRandomString(productToAdd);
            productToAdd.StockQuantity = 1;
            var productInDB = await _productRepository.AddProductAsync(productToAdd);
            var mainImagePath = _fileService.SaveImage(product.MainImage).Item2;

            List<GalleryImage> images = new List<GalleryImage>();
            product.GalleryImages.ForEach(image =>
            {
                string path = _fileService.SaveImage(image).Item2;
                if (path != null)
                {
                    GalleryImage galleryImage = new GalleryImage();
                    galleryImage.SourceUrl = path;
                    galleryImage.Product = productInDB;
                    galleryImage.ProductId = productInDB.ProductId;
                    images.Add(galleryImage);
                }
            });
            ProductImage Mainimage = new ProductImage(
                productInDB.ProductId,
                mainImagePath,
                productInDB
            );
            productToAdd.ProductImage = Mainimage;
            productToAdd.GalleryImages = images;
            var productWithCategoires = await _productRepository.AddProductToGategoeryByNameAsync(
                productToAdd,
                product.ProductCategory
            );
            var productWithAllAttrubiotes = await _productRepository.UpdateProductAsync(
                productWithCategoires
            );
            return productWithAllAttrubiotes;
        }

        public async Task<List<SimpleProductDTO>> GetAll()
        {
            var result = await _productRepository.GetAllProducts();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<List<SimpleProductDTO>> GetsimpleProductspaginated(
            int pageNumber,
            int pageSize
        )
        {
            var result = await _productRepository.GetAllProductsPagination(pageNumber, pageSize);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<SimpleProductDTO> GetSimpleProductById(int productId)
        {
            var response = await _productRepository.GetProductAsync(productId);
            return response;
        }

        public async Task<SimpleProductDTO> GetSimpleProductBySlugName(string slugName)
        {
            var response = await _productRepository.GetProductAsyncBySlugName(slugName);
            return response;
        }

        public async Task<List<SimpleProductDTO>> GetsimpleProductsInByUserId(int UserId)
        {
            var response = await _productRepository.GetProductsByUserId(UserId);
            if (response == null)
            {
                return null;
            }
            return response.ToList();
        }

        public async Task<List<SimpleProductDTO>> GetsimpleProductsInCategoryAsync(int CategoeryId)
        {
            var response = await _productRepository.GetProductsFromCategoryAsync(CategoeryId);

            return response.ToList();
        }
    }
}
