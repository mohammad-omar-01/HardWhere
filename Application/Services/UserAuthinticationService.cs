using Application.DTOs;
using Application.Repositories;
using Application.Utilities;
using Domain.User;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class UserAuthinticationService : IUserAuthicticateService
    {
        private readonly IUserAuthinticationRepoisitory _userRepository;
        private readonly IStringUtility _stringUtility;
        private readonly ITokenUtility _tokenUtility;

        public UserAuthinticationService(
            IUserAuthinticationRepoisitory userRepository,
            IStringUtility stringUtility,
            ITokenUtility tokenUtility
        )
        {
            this._userRepository = userRepository;
            this._stringUtility = stringUtility;
            this._tokenUtility = tokenUtility;
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

        public string? Login(UserSignInDTO _user)
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
                return token;
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
