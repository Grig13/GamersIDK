using GamersChatAPI.Models;
using GamersChatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamersChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCommentController : ControllerBase
    {
        private readonly PostCommentService _pcService;

        public PostCommentController(PostCommentService pcService)
        {
            _pcService = pcService;
        }

        [HttpGet]
        public IEnumerable<PostComment> Get()
        {
            return this._pcService.GetAllComments();
        }

        [HttpGet("{id}")]
        public PostComment GetComment(Guid id)
        {
            return this._pcService.GetCommentById(id);
        }

        [HttpPost]
        public PostComment Post([FromBody] PostComment postComment)
        {
            var commentToAdd = new PostComment
            {
                CommentContent = postComment.CommentContent
            };
            return this._pcService.AddComment(commentToAdd);
        }

        [HttpPut]
        public PostComment Update(Guid id, [FromBody] PostComment comment)
        {
            var commentToEdit = _pcService.GetCommentById(id);
            commentToEdit.CommentContent = comment.CommentContent;
            return this._pcService.UpdateComment(commentToEdit);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            this._pcService.DeleteComment(id);
        }
    }
}
