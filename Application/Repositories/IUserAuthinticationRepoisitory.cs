using Application.DTOs;
using Domain.User;

namespace Application.Repositories
{
    public interface IUserAuthinticationRepoisitory
    {
        User SignUpNewUser(UserSignUpDTO user);
        User GetUserByUserName(string userName);
        User GetUserByUserEmail(string email);
        User UpdateUser(User user);
    }
}
