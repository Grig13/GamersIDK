using GamersChatAPI.Models;
using GamersChatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamersChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IEnumerable<Product> Get()
        {
            return this._productService.GetAllProducts();
        }

        [HttpGet("{id}")]
        public Product GetProductById(Guid id)
        {
            return this._productService.GetProductById(id);
        }

        [HttpPost("{productId}/add-comment/{commentId}")]
        public Product AddCommentToProduct(Guid productId, Guid commentId)
        {
            var comment = _pcService.GetCommentById(commentId);
            return this._productService.AddCommentToProduct(productId, comment);
        }

        [HttpPost]
        public Product Post(Product product)
        {
            var productToAdd = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Comments = product.Comments
            };
            return this._productService.AddProduct(productToAdd);
        }

        [HttpDelete("{productId}/{commentId}")]
        public Product RemoveCommentFromProduct(Guid productId, Guid commentId)
        {
            var commentToRemove = new ProductComment();
            commentToRemove = _pcService.GetCommentById(commentId);
            return this._productService.RemoveCommentFromProduct(productId, commentToRemove);
        }

        [HttpPut("{id}")]
        public Product Update(Guid id, [FromBody] Product product)
        {
            var productToEdit = _productService.GetProductById(id);
            productToEdit.Name = product.Name;
            productToEdit.Price = product.Price;
            productToEdit.Description = product.Description;
            return this._productService.ProductUpdate(productToEdit);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            this._productService.DeleteProduct(id);
        }
    }
}
