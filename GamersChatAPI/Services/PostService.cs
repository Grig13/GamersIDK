using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;

namespace GamersChatAPI.Services
{
    public class PostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return this.postRepository.GetAll();
        }

        public Post GetPostById(Guid postId)
        {
            return this.postRepository.GetById(postId);
        }

        public Post AddPost(Post postToAdd)
        {
            return this.postRepository.Add(postToAdd);
        }

        public Post AddCommentsToPost(Guid postId, List<PostComment> comments)
        {
            return this.postRepository.AddCommentsToPost(postId, comments);
        }

        public Post AddCommentToPost(Guid postId, PostComment comment)
        {
            return this.postRepository.AddCommentToPost(postId, comment);
        }

        public Post UpdatePost(Post postToUpdate)
        {
            return this.postRepository.Update(postToUpdate);
        }

        public Post RemoveCommentFromPost(Guid postId, PostComment comment)
        {
            return this.postRepository.RemoveCommentFromPost(postId, comment);
        }

        public void DeletePost(Guid id)
        {
            this.postRepository.DeleteById(id);
        }
    }
}
