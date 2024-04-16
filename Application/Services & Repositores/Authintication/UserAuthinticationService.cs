using Application.DTOs;
using Application.DTOs.User;
using Application.Repositories;
using Application.Services___Repositores.Mail;
using Application.Utilities;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Text;

namespace Application.Services.Authintication
{
    public class UserAuthinticationService : IUserAuthicticateService
    {
        private readonly IUserAuthinticationRepoisitory _userRepository;
        private readonly IStringUtility _stringUtility;
        private readonly ITokenUtility _tokenUtility;
        private readonly IMailService _mailService;

        public UserAuthinticationService(
            IUserAuthinticationRepoisitory userRepository,
            IStringUtility stringUtility,
            ITokenUtility tokenUtility,
            IMailService mailService
        )
        {
            _userRepository = userRepository;
            _stringUtility = stringUtility;
            _tokenUtility = tokenUtility;
            _mailService = mailService;
        }

        public Task<string> ForgotPassword(string email)
        {
            var user = _userRepository.GetUserByUserEmail(email);
            if (user == null)
            {
                return Task.FromResult("");
            }

            var randomPasswordCodeGenerated = _stringUtility.GenerateRandomPassword(6);

            MailData data = new MailData();
            data.EmailSubject = "Retrive your password at HardWhere.ps";
            data.EmailToName = user.FirstName + " " + user.LastName;
            data.EmailToId = user.Email;

            StringBuilder emailBody = new StringBuilder();

            emailBody.AppendLine($"Dear {user.UserName},");
            emailBody.AppendLine();
            emailBody.AppendLine(
                "We received a request to reset your password. Your new password is:"
            );
            emailBody.AppendLine();
            emailBody.AppendLine($"{randomPasswordCodeGenerated}");
            emailBody.AppendLine();
            emailBody.AppendLine(
                "Please use this password to log in to your account. For security reasons, we recommend changing your password after logging in."
            );
            emailBody.AppendLine();
            emailBody.AppendLine(
                "If you did not request a password reset or have any concerns, please contact our support team."
            );
            emailBody.AppendLine();
            emailBody.AppendLine("Thank you,");
            emailBody.AppendLine("HardWhere.ps Support Team");

            data.EmailBody = emailBody.ToString();

            var isSuccsess = _mailService.SendMail(data);
            if (isSuccsess)
            {
                return Task.FromResult(_stringUtility.HashString(randomPasswordCodeGenerated));
            }

            return Task.FromResult("");
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
            if (user == null)
            {
                return LoginCasesEnum.INVALID_USERNAME.ToString();
            }
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

            return LoginCasesEnum.INVALID_PASSWORD.ToString();
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

        public Task<bool> UpdatePassword(UpdatePasswordRequest request)
        {
            var user = _userRepository.GetUserByUserId(request.userId);
            if (user != null)
            {
                if (user.Password.Equals(request.oldPassword))
                {
                    user.Password = request.newPassword;

                    _userRepository.UpdateUser(user);
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }
            }
            return Task.FromResult(false);
        }

        public Task<bool> UpdatePasswordDirectly(UpdatePasswordRequestDirect request)
        {
            var user = _userRepository.GetUserByUserEmail(request.email);
            if (user != null)
            {
                user.Password = (request.newPassword);

                _userRepository.UpdateUser(user);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
