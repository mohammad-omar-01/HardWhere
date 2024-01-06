using Domain.CartNS;

namespace Application.Repositories
{
    public interface ICartRepository
    {
        public Task<Cart> AddNewCart(Cart cart);
        public Task<Cart> GetCartByCartId(int cartId);
        public Task<Cart> GetCartByUserId(int userId);
        public Task<Cart> UpdateCart(int cartId, Cart cart);
        public Task<Cart> DeleteCart(int cartId);
    }
}
