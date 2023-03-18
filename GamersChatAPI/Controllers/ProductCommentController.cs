using GamersChatAPI.Models;
using GamersChatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamersChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCommentController : ControllerBase
    {
        private readonly ProductCommentService _pcService;

        public ProductCommentController(ProductCommentService pcService)
        {
            _pcService = pcService;
        }

        [HttpGet]
        public IEnumerable<ProductComment> Get()
        {
            return this._pcService.GetAllProductComments();
        }

        [HttpGet("{id}")]
        public ProductComment GetCommentById(Guid id)
        {
            return this._pcService.GetCommentById(id);
        }

        [HttpPost]
        public ProductComment Post([FromBody] ProductComment productComment)
        {
            var productCommentToAdd = new ProductComment
            {
                CommentContent = productComment.CommentContent,
                Grade = productComment.Grade
            };

            return this._pcService.AddComment(productCommentToAdd);
        }

        [HttpPut("{id}")]
        public ProductComment Update(Guid id, [FromBody] ProductComment comment)
        {
            var commentToEdit = _pcService.GetCommentById(id);
            commentToEdit.CommentContent = comment.CommentContent;
            commentToEdit.Grade = comment.Grade;
            return this._pcService.UpdateComment(commentToEdit);
        }


        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            this._pcService.RemoveComment(id);
        }
    }
}
