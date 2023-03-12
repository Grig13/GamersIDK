using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        protected readonly GamersChatDbContext _dbContext;

        public PostRepository(GamersChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Post AddCommentsToPost(Guid postId, List<PostComment> comments)
        {
            var post = GetById(postId);
            foreach (var comment in comments)
            {
                post.PostComments.Add(comment);
            }
            _dbContext.SaveChanges();
            return post;
        }

        public Post Add(Post postToAdd)
        {
            var post = this._dbContext.Add<Post>(postToAdd);
            this._dbContext.SaveChanges();
            return post.Entity;
        }

        public Post AddCommentToPost(Guid postId, PostComment commentToAdd)
        {
            var post = GetById(postId);
            post.PostComments.Add(commentToAdd);
            _dbContext.SaveChanges();
            return post;
        }

        public void DeleteById(Guid id)
        {
            var post = GetById(id);
            _dbContext.Set<Post>().Remove(post);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Post> GetAll()
        {
            return _dbContext.Set<Post>().Include(a => a.PostContent).Include(b => b.PostImage).Include(c => c.PostComments).ToList();
        }

        public Post GetById(Guid id)
        {
            var postToReturn = _dbContext.Set<Post>().Where(a => a.Id == id).Include(b => b.UserId).FirstOrDefault();
            if (postToReturn == null)
            {
                throw new KeyNotFoundException("Post not found.");
            }
            return postToReturn;
        }

        public Post RemoveCommentFromPost(Guid postId, PostComment commentToRemove)
        {
            var post = GetById(postId);
            post.PostComments.Remove(commentToRemove);
            _dbContext.SaveChanges();
            return post;
        }

        public Post Update(Post postToUpdate)
        {
            _dbContext.Set<Post>().Update(postToUpdate);
            _dbContext.SaveChanges();
            return postToUpdate;
        }
    }
}
