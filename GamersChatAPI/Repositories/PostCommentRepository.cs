using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Repositories
{
    public class PostCommentRepository : IPostCommentRepository
    {
        protected readonly GamersChatDbContext _dbContext;

        public PostCommentRepository(GamersChatDbContext context)
        {
            _dbContext = context;
        }

        public void DeleteById(Guid id)
        {
            var postCommentToBeDeleted = GetById(id);
            _dbContext.Set<PostComment>().Remove(postCommentToBeDeleted);
            _dbContext.SaveChanges();
        }

        public IEnumerable<PostComment> GetAll()
        {
            return _dbContext.Set<PostComment>().Include(a => a.CommentContent).Include(b => b.Post).ToList();
        }

        public PostComment Add(PostComment commentToAdd)
        {
            var comment = this._dbContext.Set<PostComment>().Add(commentToAdd);
            this._dbContext.SaveChanges();
            return comment.Entity;
        }

        public PostComment GetById(Guid id)
        {
            var postCommentToReturn = _dbContext.Set<PostComment>().Where(a => a.Id == id).Include(b => b.UserId).Include(c => c.UserId).FirstOrDefault();
            if (postCommentToReturn == null)
            {
                throw new KeyNotFoundException("Post Comment not found.");
            }
            return postCommentToReturn;
        }

        public PostComment Update(PostComment commentToUpdate)
        {
            _dbContext.Set<PostComment>().Update(commentToUpdate);
            _dbContext.SaveChanges();
            return commentToUpdate;
        }
    }
}
