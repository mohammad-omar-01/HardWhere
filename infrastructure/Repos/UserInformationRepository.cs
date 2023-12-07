using Application.DTOs.UserType;
using Application.Repositories;
using Application.Utilities;
using AutoMapper;
using Domain.UserType;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Repos
{
    public class UserInformationRepository : IUserInformationRepository
    {
        private readonly HardwhereDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IStringUtility _stringUtility;

        public UserInformationRepository(
            HardwhereDbContext dbContext,
            IMapper mapper,
            IStringUtility stringUtility
        )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _stringUtility = stringUtility;
        }

        public async Task<CustomerDTO> GetCustomerInfo(string username)
        {
            var user = await _dbContext.Users
                .Include(a => a.Billing)
                .Include(a => a.Shipping)
                .FirstOrDefaultAsync(var => var.UserName == username);
            return _mapper.Map<CustomerDTO>(user);
        }

        public async Task<ViewerDTO> GetViewerInfo(string username)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(var => var.UserName == username);
            var viewer = _mapper.Map<ViewerDTO>(user);
            viewer.Nicename = _stringUtility.NicenName(viewer.FirstName, viewer.LastName);
            return viewer;
        }
    }
}
