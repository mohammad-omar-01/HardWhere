using Application.DTOs.User;
using Application.Repositories;
using AutoMapper;
using Domain.UserNS;

namespace infrastructure.Repos
{
    public class UserAuthiticationRepository : IUserAuthinticationRepoisitory
    {
        private readonly HardwhereDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserAuthiticationRepository(HardwhereDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public User SignUpNewUser(UserSignUpDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            var addedUser = _dbContext.Users.Add(user);
            Address address = new Address();
            address.Address1 = string.Empty;
            address.user = addedUser.Entity;
            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();
            return user;
        }

        public User GetUserByUserName(string userName)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserName == userName);
            return user;
        }

        public User GetUserByUserEmail(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
            return user;
        }

        public User UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges(true);
            return user;
        }
    }
}
