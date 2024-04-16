using Domain.UserNS;

namespace Application.Utilities
{
    public interface ITokenUtility
    {
        public string GenerateJwtToken(User user);
    }
}
