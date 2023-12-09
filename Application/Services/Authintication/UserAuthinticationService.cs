using Application.DTOs;
using Application.Repositories;
using Application.Utilities;
using AutoMapper;
using Domain.User;
using Domain.UserType;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Authintication
{
    public class UserAuthinticationService : IUserAuthicticateService
    {
        private readonly IUserAuthinticationRepoisitory _userRepository;
        private readonly IStringUtility _stringUtility;
        private readonly ITokenUtility _tokenUtility;
        private static IMapper _mapper;

        public UserAuthinticationService(
            IUserAuthinticationRepoisitory userRepository,
            IStringUtility stringUtility,
            ITokenUtility tokenUtility
        )
        {
            _userRepository = userRepository;
            _stringUtility = stringUtility;
            _tokenUtility = tokenUtility;
        }

        public bool IsEmailAvaliable(string Email)
        {
            if (_userRepository.GetUserByUserEmail(Email) == null)
                return false;
            return true;
        }

        public bool IsUsernameAvaliable(string username)
        {
            var user = _userRepository.GetUserByUserName(username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public static IMapper GetMapper()
        {
            if (_mapper == null)
            {
                _mapper = ServiceMapper.Configure();
            }

            return _mapper;
        }

        public LoginResult? Login(UserSignInDTO _user)
        {
            var user = _userRepository.GetUserByUserName(_user.UserName);

            if (
                user.Password != null
                && _stringUtility.VerifyEquailityForTwoPasswords(user.Password, _user.Password)
            )
            {
                var token = _tokenUtility.GenerateJwtToken(user);
                user.Token = token;
                _userRepository.UpdateUser(user);
                var mapper = GetMapper();
                var destination = mapper.Map<CustomerDTO>(user);
                return new LoginResult { Token = token, AccountDetails = destination };
            }

            return null;
        }

        public bool Logout(LogoutRequestDTO logoutRequest)
        {
            var user = _userRepository.GetUserByUserName(logoutRequest.UserName);
            if (user.Token.IsNullOrEmpty())
            {
                return false;
            }
            user.Token = null;
            _userRepository.UpdateUser(user);
            return true;
        }

        public bool SignUp(UserSignUpDTO user)
        {
            var userNameAvalaible = IsUsernameAvaliable(user.UserName);
            var userEmailAvalaible = IsEmailAvaliable(user.Email);
            if (!userNameAvalaible && userEmailAvalaible)
            {
                return false;
            }
            user.Password = _stringUtility.HashString(user.Password);
            _userRepository.SignUpNewUser(user);
            return true;
        }
    }
}
