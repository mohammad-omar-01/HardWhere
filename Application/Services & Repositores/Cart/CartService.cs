using Application.DTOs.Cart;
using Application.Repositories;
using Domain.CartNS;

namespace Application.Services.CartNS
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }

        public async Task<CartDtoReturnResult> AddNewCart(CartDTO cart)
        {
            var cartToCheck = await _cartRepository.GetCartByUserId(cart.userId);

            if (cartToCheck == null)
            {
                var _mapper = ApplicationMapper.InitializeAutomapper();
                var cartToAdd = _mapper.Map<Cart>(cart);
                cartToAdd.carItemscount = (int)cart.contents.itemCount;
                cartToAdd = await _cartRepository.AddNewCart(cartToAdd);
                var cartToReturn = _mapper.Map<CartDTO>(cartToAdd);

                CartContents contents = GetContents(cartToAdd);
                cartToReturn.contents = contents;

                return new CartDtoReturnResult { result = cartToReturn, CartId = cartToAdd.cartId };
            }
            return null;
        }

        public async Task<CartDtoReturnResult> deleteCart(Cart cart)
        {
            var cartToDelete = await _cartRepository.GetCartByUserId(cart.userId);
            if (cartToDelete == null)
            {
                return null;
            }

            var delatedCart = await _cartRepository.DeleteCart(cartToDelete.cartId);
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var cartToReturn = _mapper.Map<CartDTO>(delatedCart);

            CartContents contents = GetContents(cartToDelete);
            cartToReturn.contents = contents;

            return new CartDtoReturnResult { result = cartToReturn, CartId = cartToDelete.cartId };
        }

        public async Task<CartDtoReturnResult> deleteCartById(int cartId)
        {
            var cartToDelete = await _cartRepository.GetCartByCartId(cartId);
            if (cartToDelete == null)
            {
                return null;
            }
            var delatedCart = await _cartRepository.DeleteCart(cartToDelete.cartId);
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var cartToReturn = _mapper.Map<CartDTO>(delatedCart);

            CartContents contents = GetContents(delatedCart);
            cartToReturn.contents = contents;

            return new CartDtoReturnResult { result = cartToReturn, CartId = cartToDelete.cartId };
        }

        public async Task<CartDtoReturnResult> GetCartByID(int id)
        {
            var cart = await _cartRepository.GetCartByCartId(id);
            if (cart == null)
            {
                return null;
            }
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var cartToReturn = _mapper.Map<CartDTO>(cart);
            // Assuming you have a Cart instance called "cart" with its contents
            CartContents contents = GetContents(cart);

            cartToReturn.contents = contents;
            cartToReturn.totalInt = contents.nodes.Sum(a => a.totalPriceForTheseProducts);
            cartToReturn.total = cartToReturn.totalInt?.ToString();
            return new CartDtoReturnResult { result = cartToReturn, CartId = id };
        }

        static CartContents GetContents(Cart cart)
        {
            return new CartContents
            {
                itemCount = cart.carItemscount,
                productCount = cart.contents?.Count,
                nodes = cart.contents
                    ?.Select(
                        item =>
                            new CartItem
                            {
                                quantity = item.quantity,
                                key = item.ProductId.ToString(),
                                product = new ProductRequestDTO
                                {
                                    productId = item.ProductId,
                                    name = item.CartProductName,
                                    price = "₪" + item.price.ToString(),
                                    priceRegular = item.price.ToString(),
                                    slug = "",
                                    productImage =
                                        item.ProductImage != null
                                            ? new ProductImageDTO { sourceUrl = item.ProductImage }
                                            : null
                                },
                                priceRegular = item.price.ToString(),
                                totalPriceForTheseProducts = item.price * item.quantity // Assuming TotalPriceForTheseProducts is calculated based on price and quantity
                            }
                    )
                    .ToList()
            };
        }

        public async Task<CartDtoReturnResult> GetCartByUserID(int userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            if (cart != null)
            {
                var _mapper = ApplicationMapper.InitializeAutomapper();
                var cartToReturn = _mapper.Map<CartDTO>(cart);
                CartContents contents = GetContents(cart);

                cartToReturn.contents = contents;
                cartToReturn.totalInt = contents.nodes.Sum(a => a.totalPriceForTheseProducts);
                cartToReturn.total = cartToReturn.totalInt?.ToString();
                return new CartDtoReturnResult { result = cartToReturn, CartId = cart.cartId };
            }
            return null;
        }

        public async Task<CartDtoReturnResult> UpdateCart(int cartId, CartDTO cart)
        {
            var cartToCheck = await _cartRepository.GetCartByCartId(cartId);
            if (cartToCheck == null)
            {
                return null;
            }
            var _mapper = ApplicationMapper.InitializeAutomapper();
            var cartToUpdate = _mapper.Map<Cart>(cart);
            cartToUpdate.carItemscount = (int)cart.contents.itemCount;

            cartToUpdate.contents.ForEach(a => a.CartId = cartId);
            foreach (var item in cartToUpdate.contents)
            {
                item.price = int.Parse(
                    cart.contents.nodes
                        .Where(a => a.key.Equals(item.ProductId.ToString()))
                        .Select(a => a.priceRegular.Split(".")[0])
                        .FirstOrDefault()
                );
            }

            var updatedCart = await _cartRepository.UpdateCart(cartId, cartToUpdate);
            var cartToReturn = _mapper.Map<CartDTO>(updatedCart);
            CartContents contents = GetContents(updatedCart);
            cartToReturn.totalInt = contents.nodes.Sum(a => a.totalPriceForTheseProducts);
            cartToReturn.total = cartToReturn.totalInt?.ToString();

            cartToReturn.contents = contents;
            return new CartDtoReturnResult { result = cartToReturn, CartId = cartToCheck.cartId };
        }

        public Task<Cart> AddNewCartEmpty(int UserId)
        {
            var cart = _cartRepository.AddNewCartEmpty(UserId);
            return cart;
        }
    }
}
