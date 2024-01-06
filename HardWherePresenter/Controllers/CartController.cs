using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HardWherePresenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
        }

        [HttpPost]
        public async Task<ActionResult<CartDTO>> AddCart([FromBody] CartDTO cart)
        {
            var response = await _cartService.AddNewCart(cart);
            if (response == null)
            {
                return BadRequest("Cart already exists");
            }
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }

        [HttpDelete("{cartId}")]
        public async Task<ActionResult<CartDTO>> DeleteCart([FromRoute] int cartId)
        {
            var response = await _cartService.deleteCartById(cartId);
            if (response == null)
            {
                return NotFound("Cart Not Found");
            }
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }

        [HttpGet("{cartId}")]
        public async Task<ActionResult<CartDTO>> GetCartById([FromRoute] int cartId)
        {
            var response = await _cartService.GetCartByID(cartId);
            if (response == null)
            {
                return NotFound();
            }
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }

        [HttpGet("User/{userId}")]
        public async Task<ActionResult<CartDTO>> GetCartByUserId([FromRoute] int userId)
        {
            var response = await _cartService.GetCartByUserID(userId);
            if (response == null)
            {
                return NotFound();
            }
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }

        [HttpPut("{cartId}")]
        public async Task<ActionResult<CartDTO>> UpdateCart([FromRoute] int cartId, CartDTO cart)
        {
            var response = await _cartService.UpdateCart(cartId, cart);
            if (response == null)
            {
                return BadRequest("Cart Not Found");
            }
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }
    }
}
