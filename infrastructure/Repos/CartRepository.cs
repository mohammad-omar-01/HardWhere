using Application.Repositories;
using AutoMapper;
using Domain.CartNS;
using Domain.ProductNS;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace infrastructure.Repos
{
    public class CartRepository : ICartRepository
    {
        private readonly HardwhereDbContext _dbContext;

        public CartRepository(HardwhereDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cart> AddNewCart(Cart cart)
        {
            var addedCart = await _dbContext.Carts.AddAsync(cart);

            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> DeleteCart(int cartId)
        {
            var cart = await _dbContext.Carts.FirstOrDefaultAsync(c => c.cartId == cartId);

            _dbContext.Carts.Remove(cart);
            _dbContext.SaveChanges();

            return cart;
        }

        public async Task<Cart> GetCartByCartId(int cartId)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.contents)
                .FirstOrDefaultAsync(a => a.cartId == cartId);

            return cart;
        }

        public async Task<Cart> GetCartByUserId(int userId)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.contents)
                .FirstOrDefaultAsync(a => a.userId == userId);

            return cart;
        }

        public async Task<Cart> UpdateCart(int cartId, Cart cart)
        {
            var cartToUpdate = await _dbContext.Carts.FirstOrDefaultAsync(c => c.cartId == cartId);
            var conntents = await _dbContext.CartContents.FirstOrDefaultAsync(
                c => c.CartId == cartId
            );
            cartToUpdate.contents = cart.contents;
            cartToUpdate.carItemscount = cart.carItemscount;
            cartToUpdate.total = cart.total;
            cartToUpdate.isEmpty = cart.isEmpty;

            _dbContext.SaveChanges();

            return cartToUpdate;
        }
    }
}
