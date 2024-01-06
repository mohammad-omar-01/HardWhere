﻿using Application.DTOs;
using Domain.CartNS;

namespace Application.Services
{
    public interface ICartService
    {
        public Task<CartDtoReturnResult> GetCartByID(int id);
        public Task<CartDtoReturnResult> GetCartByUserID(int userId);
        public Task<CartDtoReturnResult> AddNewCart(CartDTO cart);
        public Task<CartDtoReturnResult> UpdateCart(int cartId, CartDTO cart);
        public Task<CartDtoReturnResult> deleteCart(Cart cart);
        public Task<CartDtoReturnResult> deleteCartById(int cartId);
    }
}
