using GamersChatAPI.Models;
using GamersChatAPI.Repositories.Interfaces;

namespace GamersChatAPI.Services
{
    public class ProductCommentService
    {
        private readonly IProductCommentRepository pcRepository;

        public ProductCommentService(IProductCommentRepository pcRepository)
        {
            this.pcRepository = pcRepository;
        }

        public IEnumerable<ProductComment> GetCommentsByProductId(Guid productId)
        {
            return this.pcRepository.GetCommentsByProductId(productId);
        }

        public ProductComment GetCommentById(Guid id)
        {
            return this.pcRepository.GetById(id);
        }

        public void AddComment(ProductComment productCommentToAdd)
        {
            this.pcRepository.Add(productCommentToAdd);
        }

        public void UpdateComment(ProductComment commentToUpdate)
        {
              this.pcRepository.Update(commentToUpdate);
        }

        public void RemoveComment(Guid id)
        {
            this.pcRepository.DeleteById(id);
        }
    }
}
