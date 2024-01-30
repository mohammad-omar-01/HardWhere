using Application.DTOs.ProductDTO;
using Application.Repositories;
using AutoMapper;
using Domain.ProductNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace infrastructure.Repos
{
    public class ProductRepository : IProductRepository
    {
        private readonly HardwhereDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(HardwhereDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            var response = _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return response.Entity;
        }

        public async Task<SimpleProductDTO> GetProductAsync(int ProductId)
        {
            try
            {
                var product = await _dbContext.Products
                    .AsNoTracking()
                    .Include(p => p.GalleryImages)
                    .Include(p => p.Categories)
                    .Include(p => p.ProductImage)
                    .FirstOrDefaultAsync(p => ProductId == p.ProductId);

                if (product == null)
                {
                    return null;
                }
                else
                {
                    return _mapper.Map<SimpleProductDTO>(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<SimpleProductDTO>> GetProductsFromCategoryAsync(int categoryId)
        {
            var productsInCategory = await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Categories.Any(pc => pc.Id == categoryId))
                .Include(p => p.GalleryImages)
                .Include(p => p.Categories)
                .Include(p => p.ProductImage)
                .ToListAsync();
            if (productsInCategory.Count == 0)
            {
                return null;
            }
            var list = _mapper.Map<List<SimpleProductDTO>>(productsInCategory);

            return list;
        }

        public async Task<List<SimpleProductDTO>> GetProductsByUserId(int UserId)
        {
            var productsForUser = await _dbContext.Products
                .AsNoTracking()
                .Include(p => p.GalleryImages)
                .Include(p => p.Categories)
                .Include(p => p.ProductImage)
                .Where(p => p.UserID == UserId)
                .ToListAsync();
            if (productsForUser.Count == 0)
            {
                return null;
            }
            var list = _mapper.Map<List<SimpleProductDTO>>(productsForUser);

            return list;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var productToUpdate = await _dbContext.Products.FirstOrDefaultAsync(
                p => p.ProductId == product.ProductId
            );
            productToUpdate = product;
            await _dbContext.SaveChangesAsync();
            return productToUpdate;
        }

        public Task<List<SimpleProductDTO>> GetProductsForUserSeraches(int userId)
        {
            var userSearches = _dbContext.UserSearch.FirstOrDefault(a => a.userId == userId);
            if (userSearches == null)
            {
                return null;
            }
            var listOfProducts = _dbContext.Products
                .Include(p => p.ProductImage)
                .AsEnumerable()
                .Where(
                    product =>
                        userSearches.serachKeywords.Any(
                            keyword => product.Name.ToLower().Contains(keyword.ToLower())
                        )
                )
                .ToList();
            return Task.FromResult(_mapper.Map<List<SimpleProductDTO>>(listOfProducts));
        }

        public async Task<Product> AddProductToGategoeryByNameAsync(
            Product product,
            IEnumerable<string> Categories
        )
        {
            ProductCategory productCategory = new ProductCategory();
            foreach (var category in Categories)
            {
                productCategory = await _dbContext.ProductCategories.FirstOrDefaultAsync(
                    c => c.Name.Equals(category)
                );

                if (productCategory != null)
                {
                    if (product.Categories.FirstOrDefault(c => c.Name == category) == null)
                    {
                        product.Categories.Add(productCategory);
                    }
                }
            }
            _dbContext.SaveChanges();
            return product;
        }

        public async Task<SimpleProductDTO> GetProductAsyncBySlugName(string slugName)
        {
            try
            {
                var product = await _dbContext.Products
                    .AsNoTracking()
                    .Include(p => p.GalleryImages)
                    .Include(p => p.Categories)
                    .Include(p => p.ProductImage)
                    .FirstOrDefaultAsync(p => slugName == p.Slug);

                if (product == null)
                {
                    return null;
                }
                else
                {
                    return _mapper.Map<SimpleProductDTO>(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<SimpleProductDTO>> GetAllProducts()
        {
            try
            {
                var products = await _dbContext.Products
                    .Include(p => p.GalleryImages)
                    .Include(p => p.Categories)
                    .Include(p => p.ProductImage)
                    .Where(
                        p => p.prdouctApprovalStatus == Domain.Enums.PrdouctApprovalStatus.Approved
                    )
                    .ToListAsync();

                if (products == null)
                {
                    return null;
                }
                else
                {
                    return _mapper.Map<List<SimpleProductDTO>>(products);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Task<bool> UpdateProductImageAsync(string mainimage, List<string> Gimages, int id)
        {
            var productToUpdateImagesFor = _dbContext.Products.FirstOrDefault(
                a => a.ProductId == id
            );
            if (productToUpdateImagesFor == null)
            {
                return Task.FromResult(false);
            }
            List<GalleryImage> images = new List<GalleryImage>();

            if (!mainimage.IsNullOrEmpty())
            {
                ProductImage Mainimage = new ProductImage(id, mainimage, productToUpdateImagesFor);
                productToUpdateImagesFor.ProductImage = Mainimage;
            }
            if (Gimages.Count > 0)
            {
                Gimages.ForEach(image =>
                {
                    if (image != null)
                    {
                        GalleryImage galleryImage = new GalleryImage();
                        galleryImage.SourceUrl = image;
                        galleryImage.Product = productToUpdateImagesFor;
                        galleryImage.ProductId = id;
                        images.Add(galleryImage);
                    }
                });
                productToUpdateImagesFor.GalleryImages = images;
            }
            _dbContext.SaveChanges();
            return Task.FromResult(true);
        }

        public async Task<List<SimpleProductDTO>> GetAllProductsPagination(
            int pageNumber,
            int pageSize
        )
        {
            int recordsToSkip = (pageNumber - 1) * pageSize;

            var paginatedProducts = await _dbContext.Products
                .Include(p => p.Categories)
                .Include(p => p.ProductImage)
                .OrderBy(b => b.ProductId)
                .Skip(recordsToSkip)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<SimpleProductDTO>>(paginatedProducts);
        }

        public Task<bool> DeleteProductById(int ProductId)
        {
            var productToCheck = _dbContext.Products.FirstOrDefault(a => a.ProductId == ProductId);
            if (productToCheck != null)
            {
                _dbContext.Products.Remove(productToCheck);
                _dbContext.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
