﻿using Application.DTOs.ProductDTO;
using Application.Repositories;
using AutoMapper;
using Domain.ProductNS;
using Microsoft.EntityFrameworkCore;

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
            var response = _dbContext.Products.AddAsync(product).AsTask().Result;

            await _dbContext.SaveChangesAsync();

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

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var productToUpdate = await _dbContext.Products.FirstOrDefaultAsync(
                p => p.ProductId == product.ProductId
            );
            productToUpdate = product;
            await _dbContext.SaveChangesAsync();
            return productToUpdate;
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
            await _dbContext.SaveChangesAsync();
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
    }
}
