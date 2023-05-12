using GamersChatAPI.Models;
using GamersChatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamersChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        private readonly ProductService _productService;

        public CartController(CartService cartService, ProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        [HttpPost("{userId}/products/{productId}")]
        public IActionResult AddToCart(Guid userId, Guid productId)
        {
            _cartService.AddToCart(userId, productId);
            return Ok();
        }

        [HttpDelete("{userId}/products/{productId}")]
        public IActionResult RemoveFromCart(Guid userId, Guid productId)
        {
            _cartService.RemoveFromCart(userId, productId);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public IActionResult ClearCart(Guid userId)
        {
            _cartService.ClearCart(userId);
            return Ok();
        }
    }
}
