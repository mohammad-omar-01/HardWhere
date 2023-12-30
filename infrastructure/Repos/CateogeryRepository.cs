using Application.DTOs.Product;
using Application.DTOsNS;
using Application.DTOsNS.image;
using Application.Repositories;
using AutoMapper;
using Domain.ProductNS;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Repos
{
    public class CateogeryRepository : ICategoeryRepository
    {
        private readonly HardwhereDbContext _dbContext;
        private readonly IMapper _mapper;

        public CateogeryRepository(HardwhereDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CategoeryDTO>> GetCategories()
        {
            var pCategories = await _dbContext.ProductCategories
                .Include(c => c.CategoryImage)
                .ToListAsync();

            if (pCategories == null)
            {
                return null;
            }
            var catDTOs = _mapper.Map<List<CategoeryDTO>>(pCategories);

            foreach (var categoryDTO in catDTOs)
            {
                categoryDTO.image = _mapper.Map<CategoeryImageDTO>(categoryDTO.image);
            }
            return catDTOs;
        }

        public async Task<CategoeryDTO> GetCategoryBySlugNameAsync(string slug)
        {
            try
            {
                var category = await _dbContext.ProductCategories
                    .AsNoTracking()
                    .Include(p => p.Products)
                    .Include(p => p.CategoryImage)
                    .FirstOrDefaultAsync(p => p.Slug == slug);

                if (category == null)
                {
                    return null;
                }
                else
                {
                    return _mapper.Map<CategoeryDTO>(category);
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
