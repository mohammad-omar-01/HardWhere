using Application.DTOs.User;
using Domain.UserNS;

namespace Application.Repositories
{
    public interface IUserAuthinticationRepoisitory
    {
        User SignUpNewUser(UserSignUpDTO user);
        User GetUserByUserName(string userName);

        User GetUserByUserId(int userId);

        User GetUserByUserEmail(string email);
        User UpdateUser(User user);
    }
}
