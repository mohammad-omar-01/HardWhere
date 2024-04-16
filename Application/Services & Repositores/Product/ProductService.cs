using Application.DTOs.Notfication;
using Application.DTOs.Product;
using Application.DTOs.ProductDTO;
using Application.Repositories;
using Application.Services___Repositores.NotficationNS;
using Application.Utilities;
using BasicNotification;
using Domain.Enums;
using Domain.ProductNS;
using Domain.UserNS;
using HardWherePresenter;
using Microsoft.AspNetCore.SignalR;
using System.Drawing.Printing;

namespace Application.Services.ProductServiceNS
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        INotficationService _notficationService;

        private readonly IStringRandomGenarotor<SlugGenerator> _slugGenerator;
        IHubContext<NotificationHub, IClientNotificationHub> _hubContext;

        private readonly IStringRandomGenarotor<SKUGenerator> _skuGenerator;

        public ProductService(
            IProductRepository productRepository,
            IFileService fileService,
            IStringRandomGenarotor<SlugGenerator> slug,
            IStringRandomGenarotor<SKUGenerator> sku,
            IHubContext<NotificationHub, IClientNotificationHub> hubContext,
            INotficationService notficationService
        )
        {
            _productRepository = productRepository;
            _fileService = fileService;
            _slugGenerator = slug;
            _skuGenerator = sku;
            _hubContext = hubContext;
            _notficationService = notficationService;
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

        public Task<bool> UpdateProductImages(ProductUpdateImageDTO product)
        {
            var mainImagePath = "";
            if (product.MainImage != null)
            {
                mainImagePath = _fileService.SaveImage(product.MainImage).Item2;
            }

            List<string> images = new List<string>();
            if (product.GalleryImages != null)
            {
                product.GalleryImages.ForEach(image =>
                {
                    string path = _fileService.SaveImage(image).Item2;
                    if (path != null)
                    {
                        images.Add(path);
                    }
                });
            }
            return _productRepository.UpdateProductImageAsync(mainImagePath, images, product.Id);
        }

        public Task<bool> DeleteProductById(int ProductId)
        {
            return _productRepository.DeleteProductById(ProductId);
        }

        public Task<List<SimpleProductDTO>> GetProductsForUserSeraches(int userId)
        {
            return _productRepository.GetProductsForUserSeraches(userId);
        }

        public async Task<List<AdminProductDTO>> GetAllAdmin()
        {
            var result = await _productRepository.GetAllProductsAdmin();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<List<AdminProductDTO>> GetsimpleProductspaginatedAdmin(
            int pageNumber,
            int pageSize
        )
        {
            var result = await _productRepository.GetAllProductsPaginationAdmin(
                pageNumber,
                pageSize
            );
            if (result == null)
            {
                return null;
            }
            return result;
        }

        private async void NotfiyProductOwnerAboutNewProductsState(
            int userId,
            string productName,
            string status
        )
        {
            NotficationDTO notficationDTO = new NotficationDTO();
            notficationDTO.NotficationType = "New Product Status";
            notficationDTO.NotficationBody =
                $"Your Product {productName} has a new Status, it's now {status}";
            notficationDTO.userId = userId;
            notficationDTO.NotficationTitle = $"Your {productName} has a new update";
            var notif = await _notficationService.CreateNotfication(notficationDTO);
            var user = "";
            try
            {
                user = ConnectionMapping<string>._connections[userId.ToString()].LastOrDefault();
            }
            catch (Exception ex)
            {
                user = "";
            }

            await _hubContext.Clients.Client(user.ToString()).ClientReceiveNotification(notif);
        }

        private void NotfiyUsersAboutNewProducts(Product p)
        {
            NotficationDTO notficationDTO = new NotficationDTO();
            notficationDTO.NotficationType = "Product You May Like";
            notficationDTO.NotficationBody =
                $"Check this new Product that has just been added, be the first one to have it";
            notficationDTO.NotficationTitle = $"We have a new Product for you, Check it Now :)";

            notficationDTO.userId = 0;
            var users = _productRepository
                .GetUseresIdToNotfiyByAdminChangeStatusOfProduct(p)
                .Result;
            users.ForEach(a =>
            {
                notficationDTO.userId = a;
                var notif = _notficationService.CreateNotfication(notficationDTO).Result;
                var user = "";
                try
                {
                    user = ConnectionMapping<string>._connections[a.ToString()].LastOrDefault();
                    _hubContext.Clients.Client(user.ToString()).ClientReceiveNotification(notif);
                }
                catch (Exception ex)
                {
                    user = "";
                }
            });
        }

        public Task<bool> ChnageProductStatusByAdmin(int productId, string status)
        {
            var response = _productRepository.ChnageProductStatusByAdmin(productId, status);
            if (response == null)
            {
                return Task.FromResult(false);
            }
            NotfiyProductOwnerAboutNewProductsState(
                response.Result.UserID,
                response.Result.Name,
                status
            );
            if (status == PrdouctApprovalStatus.APPROVED.ToString())
            {
                NotfiyUsersAboutNewProducts(response.Result);
            }

            return Task.FromResult(true);
        }
    }
}
