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

        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return this._cartService.GetAllProductsFromCart();
        }

        [HttpGet("{id}")]
        public Cart GetCartById(Guid id)
        {
            return this._cartService.GetCartById(id);
        }

        [HttpPost("{cartId}/add-product")]
        public Cart AddProductsToCart(Guid cartId, [FromBody] List<Guid> productsId)
        {
            var products = new List<Product>();
            foreach (var id in productsId)
            {
                var productToAdd = _productService.GetProductById(id);
                products.Add(productToAdd);
            }
            return _cartService.AddProductsToCart(cartId, products);
        }

        [HttpPost("{cartId}/add-product/{productId}")]
        public Cart AddProductToCart(Guid cartId, Guid productId)
        {
            var product = _productService.GetProductById(productId);
            return this._cartService.AddProductToCart(cartId, product);
        }

        [HttpDelete("{cartId}/{ProductId}")]
        public Cart RemoveProductFromCart(Guid cartId, Guid productId)
        {
            var productToRemove = new Product();
            productToRemove = _productService.GetProductById(productId);
            return this._cartService.RemoveProductFromCart(cartId, productToRemove);
        }

        [HttpPost]
        public Cart Post([FromBody] Cart cart)
        {
            var cartToAdd = new Cart
            {
                Quantity = cart.Quantity
            };
            return this._cartService.AddCart(cartToAdd);

        }

        [HttpPut("{id}")]
        public Cart Update(Guid id, [FromBody] Cart cart)
        {
            var cartToEdit = _cartService.GetCartById(id);
            cartToEdit.Quantity = cart.Quantity;
            return this._cartService.UpdateCart(cartToEdit);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            this._cartService.DeleteCart(id);
        }
    }
}
