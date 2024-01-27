using Application.DTOs.UserType;
using Application.DTOsNS.UserType;
using Application.Repositories;
using Application.Utilities;
using AutoMapper;
using Domain.UserNS;
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
            var user = await _dbContext.Users.FirstOrDefaultAsync(var => var.UserName == username);
            return _mapper.Map<CustomerDTO>(user);
        }

        public async Task<UserInformationDTO> GetUserInformation(string username = "", int id = -1)
        {
            User user = null;
            if (id != -1)
            {
                user = _dbContext.Users.FirstOrDefault(var => var.Id == id);
            }
            else
            {
                user = _dbContext.Users.FirstOrDefault(var => var.UserName == username);
            }
            var adresses = _dbContext.Addresses.FirstOrDefault(add => add.userId == user.Id);
            var mappedShipping = _mapper.Map<ShippingAdressDTO>(adresses);
            var mappedBilling = _mapper.Map<BillingAdressDTO>(adresses);
            var mappedInfo = _mapper.Map<PersonalInformationDTO>(user);
            mappedBilling.firstName = user.FirstName;
            mappedBilling.lastName = user.LastName;
            mappedBilling.email = user.Email;
            mappedShipping.firstName = user.FirstName;
            mappedShipping.lastName = user.LastName;

            return new UserInformationDTO
            {
                billing = mappedBilling,
                shipping = mappedShipping,
                PersonalInformation = mappedInfo
            };
        }

        public async Task<ViewerDTO> GetViewerInfo(string username)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(var => var.UserName == username);
            if (user == null)
            {
                return null;
            }
            var viewer = _mapper.Map<ViewerDTO>(user);
            viewer.Nicename = _stringUtility.NicenName(viewer.FirstName, viewer.LastName);
            return viewer;
        }

        public PersonalInformationDTO UpdateUserInfomation(
            int UserId,
            PersonalInformationDTO pInformation
        )
        {
            var userToEdit = _dbContext.Users.FirstOrDefault(user => user.Id == UserId);
            if (userToEdit == null)
            {
                return null;
            }
            userToEdit.FirstName = pInformation.firstName;
            userToEdit.LastName = pInformation.lastName;
            userToEdit.Email = userToEdit.Email;
            _dbContext.SaveChanges();
            return pInformation;
        }
    }
}
