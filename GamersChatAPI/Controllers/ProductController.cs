using GamersChatAPI.Models;
using GamersChatAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamersChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ProductCommentService _pcService;

        public ProductController(ProductService productService, ProductCommentService pcService)
        {
            _productService = productService;
            _pcService = pcService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = this._productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(Guid id)
        {
            var comment =  this._productService.GetProductById(id);
            return Ok(comment);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _productService.AddProduct(product);
            return Ok();
        }

        [HttpPut("{id}")]
        public Product Update(Guid id, [FromBody] Product product)
        {
            var productToEdit = _productService.GetProductById(id);
            productToEdit.Name = product.Name;
            productToEdit.Description = product.Description;
            productToEdit.Category = product.Category;
            productToEdit.Price = product.Price;
            productToEdit.ImageUrl = product.ImageUrl;
            return this._productService.ProductUpdate(productToEdit);

        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(Guid productId)
        {
            _productService.DeleteProduct(productId);
            return Ok();
        }
    }
}
