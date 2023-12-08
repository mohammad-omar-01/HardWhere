using Application.DTOs;
using Application.DTOs.image;
using Application.Repositories;
using AutoMapper;
using Domain.Product;
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
    }
}
