using GamersChatAPI.Models;
using GamersChatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamersChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly CartItemService _cartItemService;

        public CartItemController(CartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetCartItemsByUserId(Guid userId)
        {
            var cartItems = _cartItemService.GetCartItemsByUserId(userId);
            return Ok(cartItems);
        }

        [HttpPost]
        public IActionResult AddCartItem(CartItem cartItem)
        {
            _cartItemService.AddCartItem(cartItem);
            return Ok();
        }

        [HttpDelete("{cartItemId}")]
        public IActionResult RemoveCartItem(Guid cartItemId)
        {
            _cartItemService.RemoveCartItem(cartItemId);
            return Ok();
        }
    }
}
