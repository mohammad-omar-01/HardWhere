using Domain.UserNS;

namespace Application.Repositories
{
    public interface IAddress
    {
        public Task<List<Address>> GetUserAdresses(int userId);
    }
}
