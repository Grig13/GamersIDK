using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;

namespace GamersChatAPI.Services
{
    public class PostCommentService
    {
        private readonly IPostCommentRepository pcRepository;

        public PostCommentService(IPostCommentRepository pcRepository)
        {
            this.pcRepository = pcRepository;
        }

        public IEnumerable<PostComment> GetAllComments()
        {
            return this.pcRepository.GetAll();
        }

        public PostComment GetCommentById(Guid postId)
        {
            return this.pcRepository.GetById(postId);
        }

        public void AddComment(PostComment commentToAdd)
        {
             this.pcRepository.Add(commentToAdd);
        }

        public PostComment UpdateComment(PostComment commentToUpdate)
        {
            return this.pcRepository.Update(commentToUpdate);
        }

        public void DeleteComment(Guid commentId)
        {
            this.pcRepository.DeleteById(commentId);
        }
    }
}
