using GamersChatAPI.Models;
using GamersChatAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamersChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly PostCommentService _pcService;

        public PostController(PostService postService, PostCommentService pcService)
        {
            _postService = postService;
            _pcService = pcService;
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return this._postService.GetAllPosts();
        }

        [HttpGet("{id}")]
        public Post GetPostById(Guid id)
        {
            return this._postService.GetPostById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Post post)
        {
            _postService.AddPost(post);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPost("{postId}/add-comment")]
        public Post AddCommentsToPost(Guid postId, [FromBody] List<Guid> commentsId)
        {
            var comments = new List<PostComment>();
            foreach (var commentId in commentsId)
            {
                var commentToAdd = _pcService.GetCommentById(commentId);
                comments.Add(commentToAdd);
            }
            return _postService.AddCommentsToPost(postId, comments);
        }

        [HttpPost("{postId}/add-comment/{commentId}")]
        public Post AddCommentToPost(Guid postId, Guid commentId)
        {
            var comment = _pcService.GetCommentById(commentId);
            return this._postService.AddCommentToPost(postId, comment);
        }

        [HttpDelete("{postId}/{commentId}")]
        public Post RemoveCommentFromPost(Guid postId, Guid commentId)
        {
            var commentToRemove = new PostComment();
            commentToRemove = _pcService.GetCommentById(commentId);
            return this._postService.RemoveCommentFromPost(postId, commentToRemove);
        }

        [HttpPut("{id}")]
        public Post Update(Guid id, [FromBody] Post post)
        {
            var postToEdit = _postService.GetPostById(post.Id);
            postToEdit.PostContent = post.PostContent;
            postToEdit.PostImage = post.PostImage;
            return this._postService.UpdatePost(postToEdit);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            this._postService.DeletePost(id);
        }
    }
}
