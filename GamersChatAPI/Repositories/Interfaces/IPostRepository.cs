using GamersChatAPI.Models;

namespace GamersChatAPI.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public IEnumerable<Post> GetAll();
        public Post AddCommentsToPost(Guid postId, List<PostComment> comments);
        public Post AddCommentToPost(Guid postId, PostComment commentToAdd);
        public Post RemoveCommentFromPost(Guid postId, PostComment commentToRemove);
        public Post Add(Post postToAdd);
        public void DeleteById(Guid id);
        public Post Update(Post postToUpdate);
        public Post GetById(Guid id);
    }
}
