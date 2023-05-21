using GamersChatAPI.Models;
using GamersChatAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamersChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductCommentController : ControllerBase
    {
        private readonly ProductCommentService _pcService;

        public ProductCommentController(ProductCommentService pcService)
        {
            _pcService = pcService;
        }

        [HttpGet("product/{productId}")]
        public IActionResult GetCommentsByProductId(Guid productId)
        {
            var comments = _pcService.GetCommentsByProductId(productId);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentById(Guid id)
        {
            var comment =  this._pcService.GetCommentById(id);
            return Ok(comment);
        }

        [HttpPost]
        public IActionResult AddComment(ProductComment productComment)
        {
            _pcService.AddComment(productComment);
            return CreatedAtAction(nameof(GetCommentById), new { id = productComment.Id }, productComment);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] ProductComment comment)
        {
            if (id != comment.Id)
                return BadRequest();

            _pcService.UpdateComment(comment);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            this._pcService.RemoveComment(id);
            return Ok();
        }
    }
}
